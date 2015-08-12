using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PsExecAsync {
    public class ComputerElementCollection : ConfigurationElementCollection, IEnumerable<ComputerElement>, IEnumerable {
        public override ConfigurationElementCollectionType CollectionType {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        public ComputerElement this[int index] {
            get { return (ComputerElement)base.BaseGet(index); }
            set {
                if(base.BaseGetKey(index) != null) {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(value);
            }
        }

        public void Add(ComputerElement element) {
            this.BaseAdd(element);
        }

        protected override ConfigurationElement CreateNewElement() {
            return new ComputerElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((ComputerElement)element).EndPoint;
        }

        public new IEnumerator<ComputerElement> GetEnumerator() {
            return (from i in Enumerable.Range(0, base.Count) select this[i]).GetEnumerator();
        }
    }
}