﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotCornersWin.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.10.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool IsEnabled {
            get {
                return ((bool)(this["IsEnabled"]));
            }
            set {
                this["IsEnabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("none")]
        public string LeftTop {
            get {
                return ((string)(this["LeftTop"]));
            }
            set {
                this["LeftTop"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("none")]
        public string RightTop {
            get {
                return ((string)(this["RightTop"]));
            }
            set {
                this["RightTop"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string LeftBottom {
            get {
                return ((string)(this["LeftBottom"]));
            }
            set {
                this["LeftBottom"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string RightBottom {
            get {
                return ((string)(this["RightBottom"]));
            }
            set {
                this["RightBottom"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int MultiMonCfg {
            get {
                return ((int)(this["MultiMonCfg"]));
            }
            set {
                this["MultiMonCfg"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int AreaSize {
            get {
                return ((int)(this["AreaSize"]));
            }
            set {
                this["AreaSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("75")]
        public int PollInterval {
            get {
                return ((int)(this["PollInterval"]));
            }
            set {
                this["PollInterval"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DisableOnFullscreen {
            get {
                return ((bool)(this["DisableOnFullscreen"]));
            }
            set {
                this["DisableOnFullscreen"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int DelayLT {
            get {
                return ((int)(this["DelayLT"]));
            }
            set {
                this["DelayLT"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int DelayRT {
            get {
                return ((int)(this["DelayRT"]));
            }
            set {
                this["DelayRT"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int DelayLB {
            get {
                return ((int)(this["DelayLB"]));
            }
            set {
                this["DelayLB"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int DelayRB {
            get {
                return ((int)(this["DelayRB"]));
            }
            set {
                this["DelayRB"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("[{\"$type\":\"CAShell\",\"Command\":\"calc.exe\",\"Name\":\"Calculator\"},{\"$type\":\"CAShell\"," +
            "\"Command\":\"https://github.com/flexits/\",\"Name\":\"GitHub\"},{\"$type\":\"CAShell\",\"Com" +
            "mand\":\"Shell:::{ED7BA470-8E54-465E-825C-99712043E01C}\",\"Name\":\"All Tasks\"}]")]
        public string CustomActions {
            get {
                return ((string)(this["CustomActions"]));
            }
            set {
                this["CustomActions"] = value;
            }
        }
    }
}
