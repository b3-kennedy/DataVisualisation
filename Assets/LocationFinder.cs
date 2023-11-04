using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace reversegeocoding
{


[DataContract]
 public class RootObject
 {
 [DataMember]
 public string place_id { get; set; }
 [DataMember]
 public string licence { get; set; }
 [DataMember]
 public string osm_type { get; set; }
 [DataMember]
 public string osm_id { get; set; }
 [DataMember]
 public string lat { get; set; }
 [DataMember]
 public string lon { get; set; }
 [DataMember]
 public string display_name { get; set; }
  [DataMember]
 public string city { get; set; }
 [DataMember]
 public string country { get; set; }
 }
}