module ExDocFSharp.Test
open FsUnit
open NUnit.Framework
open Moonmile.ExDoc

[<TestFixture>]
type ``Demo Fsharp test``() =
    
    [<Test>]
    member this.``loading test``() =
        
            let xml = """
<root>
    <person name='tomoaki'>masuda</person>
</root>
"""
            let doc = ExDocument.LoadXml(xml)
            doc.Root.Name |> should equal "root"
            // seek element
            let el:ExElement = doc * "person" |> ExElements.op_Implicit
            Assert.AreEqual("person", el.Name );
            Assert.AreEqual("masuda", el.Value);
            let v = el % "name"
            Assert.AreEqual("tomoaki", v.Value);




