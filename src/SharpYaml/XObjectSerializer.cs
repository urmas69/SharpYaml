

using System.Collections.Generic;
using SharpYaml.Serialization;

namespace SharpYaml;

public class DefaultSafeDict<TKey, TValue> : Dictionary<TKey, HashSet<TValue>> //where TValue : new()
{
    public new HashSet<TValue> this[TKey key]
    {
        get
        {
            if (TryGetValue(key!, out var value)) return value;
            value = new();
            base[key] = value;
            return value;
        }
        set => base[key!] = value;
    }
}
public static class XObjectSerializer
{
    private static readonly DefaultSafeDict<ITypeDescriptor, string> invalid = new();

    public static void Add(ITypeDescriptor objectContextDescriptor, string memberName)
    {
        invalid[objectContextDescriptor].Add(memberName);
    }

    public static Dictionary<ITypeDescriptor, HashSet<string>> Get() => invalid;
}
