//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STLTapReport.data
{
    using System;
    using System.Collections.Generic;
    
    public partial class style
    {
        public style()
        {
            this.beers = new HashSet<beer>();
            this.users = new HashSet<user>();
        }
    
        public int styleID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string origin { get; set; }
        public byte[] image { get; set; }
    
        public virtual ICollection<beer> beers { get; set; }
        public virtual ICollection<user> users { get; set; }
    }
}
