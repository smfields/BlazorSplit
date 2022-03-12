using System.Collections.Generic;
using System.Text.Json.Serialization;
using BlazorSplit.JsonConverters;

namespace BlazorSplit.Tests;

public partial class SingleOrArrayConverterTests
{
    #region Nested type: TestObject

    private class TestObject
    {
        [JsonConverter(typeof(SingleOrArrayJsonConverter<double>))]
        public List<double>? Values { get; set; }
    }

    #endregion
}