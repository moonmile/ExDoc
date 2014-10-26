namespace ExYamlFsTest

open FsUnit
open NUnit.Framework
open YamlDotNet
open YamlDotNet.RepresentationModel
open YamlDotNet.Core
open YamlDotNet.Serialization
open System.Xml
open System.IO
open YamlDotNet.Converters.Xml

[<TestFixture>]
type ``yaml test``() =
    [<Test>] 
    member this.``simple test``() =
        Assert.AreEqual( true, true );

        let ys = new YamlStream()
        ys.Load( new StringReader(""))
        let ydoc:YamlDocument = ys.Documents.[0]
        let conv = new XmlConverter()
        let xdoc = conv.ToXml(ydoc)
        ()



        
