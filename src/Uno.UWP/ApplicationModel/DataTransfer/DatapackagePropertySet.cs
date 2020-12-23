using System.Collections.Generic;

namespace Windows.ApplicationModel.DataTransfer
{
	public partial class DataPackagePropertySet : IDictionary<string, object>, IEnumerable<KeyValuePair<string, object>>
	{
		public string Title { get; set; }

		public string Description { get; set; }
	}
}
