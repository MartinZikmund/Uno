using System;
using Uno.Devices.Midi.Internal;
using Windows.Storage.Streams;

namespace Windows.Devices.Midi
{
	/// <summary>
	/// Represents a MIDI message that specifies a stop message.
	/// </summary>
	public partial class MidiStopMessage : IMidiMessage
	{
		/// <summary>
		/// Creates a new MidiStopMessage object.
		/// </summary>
		public MidiStopMessage()
		{
			RawData = new Storage.Streams.Buffer(new byte[]
			{
				(byte)Type
			});
		}

		internal MidiStopMessage(byte[] rawData, TimeSpan timestamp)
		{
			MidiMessageValidators.VerifyMessageLength(rawData, 1, Type);
			MidiMessageValidators.VerifyMessageType(rawData[0], Type);

			RawData = new Storage.Streams.Buffer(rawData);
			Timestamp = timestamp;
		}

		/// <summary>
		/// Gets the type of this MIDI message.
		/// </summary>
		public MidiMessageType Type => MidiMessageType.Stop;

		/// <summary>
		/// Gets the array of bytes associated with the MIDI message, including status byte.
		/// </summary>
		public IBuffer RawData { get; }

		/// <summary>
		/// Gets the duration from when the MidiInPort was created to the time the message was received.
		/// For messages being sent to a MidiOutPort, this value has no meaning.
		/// </summary>
		public TimeSpan Timestamp { get; }
	}
}
