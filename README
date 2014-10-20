# ExDoc

ExDoc is Simple Parser and Query of Xml.
This library use by Implicit cast, You can write one liner code about Xml Query. 

# Sample

```csharp
/// <summary>
/// Parse Xml string
/// </summary>
[TestMethod]
public void TestLoad()
{
    var xml = "<root><person name='tomoaki'>masuda</person></root>";

    var doc = ExDocument.LoadXml(xml);
    Assert.AreEqual("root", doc.Root.Name);
    // seek element
    ExElement el = doc * "person";
    Assert.AreEqual("person", el.Name );
    Assert.AreEqual("masuda", el.Value);
    string val = el % "name";
    Assert.AreEqual("tomoaki", val);
}

/// <summary>
/// Query Xml
/// </summary>
[TestMethod]
public void TestQuery()
{
    var xml = "<root>" + 
        "<person id='1'>masuda</person>" +  
        "<person id='2'>tomoaki</person>" +  
        "<person id='3'>yumi</person>" +  
        "</root>";

    var doc = ExDocument.LoadXml(xml);

    // query element
    ExElement el = doc * "person" % "id" == "2";
    Assert.AreEqual("tomoaki", el.Value);
    // query elements
    ExElements els = doc * "person";
    Assert.AreEqual(3, els.Count);
    Assert.AreEqual("masuda", els[0].Value);
    Assert.AreEqual("tomoaki", els[1].Value);
    Assert.AreEqual("yumi", els[2].Value);
}

/// <summary>
/// auto cast
/// </summary>
[TestMethod]
public void TestAutoCast()
{
    var xml = "<root>" +
        "<person id='1' age='20'>masuda</person>" +
        "<person id='2' age='30'>tomoaki</person>" +
        "<person id='3' age='40'>yumi</person>" +
        "</root>";

    var doc = ExDocument.LoadXml(xml);

    // query elements
    ExElement el = doc * "person" % "id" == 2;
    Assert.AreEqual(30, el % "age" );   // get attribute number
    Assert.AreEqual("tomoaki", el);     // get element value string
}
```

# License

MIT

# Author

Tomoaki Masuda

# Version

- ver.1.0 initial version


