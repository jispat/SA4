﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Muktas.ERP.Common {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Muktas.ERP.Common.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is already exists, please choose another {0}..
        /// </summary>
        public static string Already_Exists {
            get {
                return ResourceManager.GetString("Already_Exists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} must be equal to or greater than {1}..
        /// </summary>
        public static string Greater_Then {
            get {
                return ResourceManager.GetString("Greater_Then", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} must be equal to or greater than today..
        /// </summary>
        public static string Greater_Then_Today {
            get {
                return ResourceManager.GetString("Greater_Then_Today", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The argument cannot be null..
        /// </summary>
        public static string Invalid_Argument {
            get {
                return ResourceManager.GetString("Invalid_Argument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Email field is not a valid e-mail address. .
        /// </summary>
        public static string Invalid_Email {
            get {
                return ResourceManager.GetString("Invalid_Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password must be at least 6 characters long..
        /// </summary>
        public static string Invalid_Password {
            get {
                return ResourceManager.GetString("Invalid_Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mobile field is not a valid phone number..
        /// </summary>
        public static string Invalid_Phone {
            get {
                return ResourceManager.GetString("Invalid_Phone", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} information is not found..
        /// </summary>
        public static string Not_Found {
            get {
                return ResourceManager.GetString("Not_Found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Old password does not matched..
        /// </summary>
        public static string Old_Password_Not_Matched {
            get {
                return ResourceManager.GetString("Old_Password_Not_Matched", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} field is required..
        /// </summary>
        public static string Required {
            get {
                return ResourceManager.GetString("Required", resourceCulture);
            }
        }
    }
}