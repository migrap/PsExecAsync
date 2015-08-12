using System;
using System.Configuration;

namespace PsExecAsync {
    public class ComputerElement : ConfigurationElement {
        [ConfigurationProperty("EndPoint", IsKey = true, IsRequired = true)]
        public string EndPoint {
            get { return (string)this["EndPoint"]; }
            set { this["EndPoint"] = (object)value; }
        }

        [ConfigurationProperty("ExitCode", IsKey = false, IsRequired = true)]
        public int ExitCode {
            get { return (int)this["ExitCode"]; }
            set { this["ExitCode"] = (object)value; }
        }

        [ConfigurationProperty("Output", IsKey = false, IsRequired = false)]
        public string Output {
            get { return (string)this["Output"]; }
            set { this["Output"] = (object)value; }
        }

        [ConfigurationProperty("Error", IsKey = false, IsRequired = false)]
        public string Error {
            get { return (string)this["Error"]; }
            set { this["Error"] = (object)value; }
        }

        [ConfigurationProperty("StartTime", IsKey = false, IsRequired = false)]
        public DateTime StartTime {
            get { return (DateTime)this["StartTime"]; }
            set { this["StartTime"] = (object)value; }
        }

        [ConfigurationProperty("TotalProcessorTime", IsKey = false, IsRequired = false)]
        public TimeSpan TotalProcessorTime {
            get { return (TimeSpan)this["TotalProcessorTime"]; }
            set { this["TotalProcessorTime"] = (object)value; }
        }
    }
}