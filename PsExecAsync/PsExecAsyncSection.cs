using System.Configuration;

namespace PsExecAsync {
    public class PsExecAsyncSection : ConfigurationSection {
        [ConfigurationProperty("computers", IsRequired = true)]
        [ConfigurationCollection(typeof(ComputerElement), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public ComputerElementCollection Computers {
            get { return (ComputerElementCollection)this["computers"]; }
        }
    }
}