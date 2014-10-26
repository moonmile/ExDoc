# ExYaml

ExYaml is a library that convert from Yaml data to Xml.
Shaping the YAML data into easy-to-read XML format for people.

# Sample

```charp
/// <summary>
/// item data like
/// </summary>
[TestMethod]
public void TestMix2()
{
    var yaml = @"
items:
 - item: {id: 1, name: masuda }
 - item: {id: 2, name: tomoaki }
 - item: {id: 3, name: yumi }
";
    var xml = @"
<items>
  <item id=""1"" name=""masuda"" />
  <item id=""2"" name=""tomoaki"" />
  <item id=""3"" name=""yumi"" />
</items>
".Trim();

    var ys = new YamlStream();
    ys.Load(new StringReader(yaml));
    YamlDocument ydoc = ys.Documents[0];
    var dump = ydoc.Dump();
    Debug.WriteLine(dump);

    var xdoc = ydoc.ToXDocument();
    var xml1 = xdoc.ToString(SaveOptions.None);
    Debug.WriteLine(xml1);
    Assert.AreEqual(xml, xml1);
}

/// <summary>
/// setting file like
/// </summary>
[TestMethod]
public void TestMix1()
{
    var yaml = @"
app:
 - user: masuda
 - apikey:  key-xxxx
 - apipass: password
";
    var xml = @"
<app>
  <user>masuda</user>
  <apikey>key-xxxx</apikey>
  <apipass>password</apipass>
</app>
".Trim();

    var ys = new YamlStream();
    ys.Load(new StringReader(yaml));
    YamlDocument ydoc = ys.Documents[0];
    var dump = ydoc.Dump();
    Debug.WriteLine(dump);

    var xdoc = ydoc.ToXDocument();
    var xml1 = xdoc.ToString(SaveOptions.None);
    Debug.WriteLine(xml1);
    Assert.AreEqual(xml, xml1);
}

/// <summary>
/// complex data 
/// </summary>
[TestMethod]
public void TestMix3()
{
    var yaml = @"
items:
 - item: 
   id: 1
   text: item-1
- item: 
   id: 2
   text: item-2
   memo: 
    author: masuda
    date:   2014/10/26
- item: 
   id: 3
   text: item-2
";
    var xml = @"
<items>
  <item id=""1"" text=""item-1"" />
  <item id=""2"" text=""item-2"">
    <memo author=""masuda"" date=""2014/10/26"" />
  </item>
  <item id=""3"" text=""item-2"" />
</items>
".Trim();

    var ys = new YamlStream();
    ys.Load(new StringReader(yaml));
    YamlDocument ydoc = ys.Documents[0];
    var dump = ydoc.Dump();
    Debug.WriteLine(dump);

    var xdoc = ydoc.ToXDocument();
    var xml1 = xdoc.ToString(SaveOptions.None);
    Debug.WriteLine(xml1);
    Assert.AreEqual(xml, xml1);
}
```


# License

MIT

# Author

Tomoaki Masuda

# Version

- ver.1.0 initial version


