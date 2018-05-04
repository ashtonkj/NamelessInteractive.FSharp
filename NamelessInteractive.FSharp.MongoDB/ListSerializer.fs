namespace NamelessInteractive.FSharp.MongoDB.Serializers

open System.Collections.Generic
open MongoDB.Bson.Serialization
open MongoDB.Bson.Serialization.Serializers

type ListSerializer<'ElemType>() = 
    inherit SerializerBase<'ElemType list>()
    
    let serializer = EnumerableInterfaceImplementerSerializer<ResizeArray<'ElemType>, 'ElemType>()
    
    override this.Serialize(context, _, value) =
        serializer.Serialize(context, ResizeArray(value))
        
    override this.Deserialize(context, args) =
        let res = serializer.Deserialize(context, args)
        res |> unbox |> List.ofSeq<'ElemType>