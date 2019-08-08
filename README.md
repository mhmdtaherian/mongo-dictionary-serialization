# mongo-dictionary-serialization
This ASP.NET Core project provides a custom serialization to store multi-level `Dictionary<string, object>` in MongoDB.
The main issue is when we try to store `Dictionary<string, object>` in MongoDB, if the dictionary contains one level, every thing will be fine. 
But if we try to store multi-level dictionary like 
```
{
    "_id" : ObjectId("5d4a4e389451983034235c44"),
    "Name" : "My Car",
    "MyProperties" : {
        "color" : "blue",
        "wheelCount" : 4,
        "interior" : {
            "appleCarPlay" : true,
            "transmision" : "Automatic",
            "otherSpec" : {
                "color" : "Black",
                "bluetooth" : true,
                "capacity" : 5
            }
        }
    }
}
```
in DB, it will be stored in as below
```
{
    "_id" : ObjectId("5d4b955a01016a0484e1b4aa"),
    "Name" : "My Car",
    "MyProperties" : {
        "color" : "blue",
        "wheelCount" : NumberLong(4),
        "interior" : {
            "_t" : "Newtonsoft.Json.Linq.JObject, Newtonsoft.Json, Version=12.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed",
            "_v" : {
                "appleCarPlay" : {
                    "_t" : "JValue",
                    "_v" : []
                },
                "transmision" : {
                    "_t" : "JValue",
                    "_v" : []
                },
                "otherSpec" : {
                    "_t" : "JObject",
                    "_v" : [ 
                        {
                            "_t" : "JProperty",
                            "_v" : [ 
                                {
                                    "_t" : "JValue",
                                    "_v" : []
                                }
                            ]
                        }, 
                        {
                            "_t" : "JProperty",
                            "_v" : [ 
                                {
                                    "_t" : "JValue",
                                    "_v" : []
                                }
                            ]
                        }, 
                        {
                            "_t" : "JProperty",
                            "_v" : [ 
                                {
                                    "_t" : "JValue",
                                    "_v" : []
                                }
                            ]
                        }
                    ]
                }
            }
        }
    }
}
```
MongoDB serializer cannot determine the internal dictionaries and will store it as you see above. 
Now we need to implement a custom serializer and annotate it at the top of our dictionary property to fix everything.