﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace GoogleApi.Entities.Search.Common.Response
{
    /// <summary>
    /// PageMap information.
    /// </summary>
    [DataContract]
    public class PageMap
    {
        /// <summary>
        /// PageMap information, keyed by the name of this PageMap.
        /// </summary>
        [JsonProperty("list")]
        public virtual IDictionary<string, IDictionary<string, string>> List { get; set; }
    }
}