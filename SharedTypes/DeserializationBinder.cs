using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace SharedTypes {
    sealed class DeserializationBinder : SerializationBinder {
        public override Type BindToType(string assemblyName, string typeName) {
            if (typeName.Contains("System.Collections.Generic.List")) {
                return new List<PatchedFile>().GetType();
            }
            if (typeName.Contains("PatchContainer")) {
                return new PatchContainer().GetType();
            }
            if (typeName.Contains("PatchedFile")) {
                return new PatchedFile().GetType();
            }
            Debug.WriteLine($"{assemblyName} {typeName}");
            Type typeToDeserialize = null;

            // For each assemblyName/typeName that you want to deserialize to
            // a different type, set typeToDeserialize to the desired type.
            try {
                var asm = Assembly.Load(assemblyName);
                typeToDeserialize = Type.GetType(String.Format("{0}, {1}",
                    typeName, asm));
            } catch (Exception ex) {
                Debug.WriteLine(ex.ToString());
            }

            if (typeToDeserialize != null) return typeToDeserialize;

            String exeAssembly = Assembly.GetExecutingAssembly().FullName;

            // The following line of code returns the type.
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}",
                typeName, exeAssembly));


            return typeToDeserialize;
        }
    }
}
