﻿namespace Windows.Devices.Midi {
	export class MidiInPort {
		private static dispatchMessage:
			(instanceId: string, serializedMessage: string, timestamp: number) => number;

		private static instanceMap: { [managedId: string]: MidiInPort };

		private managedId: string;
		private inputPort: WebMidi.MIDIInput;

		private constructor(managedId: string, inputPort: WebMidi.MIDIInput) {
			this.managedId = managedId;
			this.inputPort = inputPort;
		}

		public static createPort(managedId: string, encodedDeviceId: string) {
			var midi = Uno.Devices.Midi.Internal.WasmMidiAccess.getMidi();
			const deviceId = decodeURIComponent(encodedDeviceId);
			var input = midi.inputs.get(deviceId);
			MidiInPort.instanceMap[managedId] = new MidiInPort(managedId, input);
		}

		public static removePort(managedId: string) {
			var instance = MidiInPort.instanceMap[managedId];
			instance.inputPort.removeEventListener("onmidimessage", instance.messageReceived);
			delete MidiInPort.instanceMap[managedId];
		}

		public static startMessageListener(managedId: string) {
			if (!this.dispatchMessage) {
				this.dispatchMessage = (<any>Module).mono_bind_static_method(
					"[Uno] Windows.Devices.Midi.MidiInPort:DispatchMessage");
			}

			const instance = MidiInPort.instanceMap[managedId];
			instance.inputPort.addEventListener("onmidimessage", instance.messageReceived);
		}

		public static stopMessageListener(managedId: string) {
			const instance = MidiInPort.instanceMap[managedId];
			instance.inputPort.removeEventListener("onmidimessage", instance.messageReceived);
		}

		private messageReceived(event: WebMidi.MIDIMessageEvent) {
			var serializedMessage = event.data[0].toString();
			for (var i = 1; i < event.data.length; i++) {
				serializedMessage += ':' + event.data[i];
			}
			MidiInPort.dispatchMessage(this.managedId, serializedMessage, event.receivedTime);
		}
	}
}