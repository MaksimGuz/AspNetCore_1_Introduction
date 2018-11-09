/* 
 * Mentoring API
 *
 * No descripton provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IO.Swagger.Model
{
    /// <summary>
    /// Suppliers
    /// </summary>
    [DataContract]
    public partial class Suppliers :  IEquatable<Suppliers>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Suppliers" /> class.
        /// </summary>
        /// <param name="SupplierId">SupplierId.</param>
        /// <param name="CompanyName">CompanyName.</param>
        /// <param name="ContactName">ContactName.</param>
        /// <param name="ContactTitle">ContactTitle.</param>
        /// <param name="Address">Address.</param>
        /// <param name="City">City.</param>
        /// <param name="Region">Region.</param>
        /// <param name="PostalCode">PostalCode.</param>
        /// <param name="Country">Country.</param>
        /// <param name="Phone">Phone.</param>
        /// <param name="Fax">Fax.</param>
        /// <param name="HomePage">HomePage.</param>
        /// <param name="Products">Products.</param>
        public Suppliers(int? SupplierId = null, string CompanyName = null, string ContactName = null, string ContactTitle = null, string Address = null, string City = null, string Region = null, string PostalCode = null, string Country = null, string Phone = null, string Fax = null, string HomePage = null, List<Products> Products = null)
        {
            this.SupplierId = SupplierId;
            this.CompanyName = CompanyName;
            this.ContactName = ContactName;
            this.ContactTitle = ContactTitle;
            this.Address = Address;
            this.City = City;
            this.Region = Region;
            this.PostalCode = PostalCode;
            this.Country = Country;
            this.Phone = Phone;
            this.Fax = Fax;
            this.HomePage = HomePage;
            this.Products = Products;
        }
        
        /// <summary>
        /// Gets or Sets SupplierId
        /// </summary>
        [DataMember(Name="supplierId", EmitDefaultValue=false)]
        public int? SupplierId { get; set; }
        /// <summary>
        /// Gets or Sets CompanyName
        /// </summary>
        [DataMember(Name="companyName", EmitDefaultValue=false)]
        public string CompanyName { get; set; }
        /// <summary>
        /// Gets or Sets ContactName
        /// </summary>
        [DataMember(Name="contactName", EmitDefaultValue=false)]
        public string ContactName { get; set; }
        /// <summary>
        /// Gets or Sets ContactTitle
        /// </summary>
        [DataMember(Name="contactTitle", EmitDefaultValue=false)]
        public string ContactTitle { get; set; }
        /// <summary>
        /// Gets or Sets Address
        /// </summary>
        [DataMember(Name="address", EmitDefaultValue=false)]
        public string Address { get; set; }
        /// <summary>
        /// Gets or Sets City
        /// </summary>
        [DataMember(Name="city", EmitDefaultValue=false)]
        public string City { get; set; }
        /// <summary>
        /// Gets or Sets Region
        /// </summary>
        [DataMember(Name="region", EmitDefaultValue=false)]
        public string Region { get; set; }
        /// <summary>
        /// Gets or Sets PostalCode
        /// </summary>
        [DataMember(Name="postalCode", EmitDefaultValue=false)]
        public string PostalCode { get; set; }
        /// <summary>
        /// Gets or Sets Country
        /// </summary>
        [DataMember(Name="country", EmitDefaultValue=false)]
        public string Country { get; set; }
        /// <summary>
        /// Gets or Sets Phone
        /// </summary>
        [DataMember(Name="phone", EmitDefaultValue=false)]
        public string Phone { get; set; }
        /// <summary>
        /// Gets or Sets Fax
        /// </summary>
        [DataMember(Name="fax", EmitDefaultValue=false)]
        public string Fax { get; set; }
        /// <summary>
        /// Gets or Sets HomePage
        /// </summary>
        [DataMember(Name="homePage", EmitDefaultValue=false)]
        public string HomePage { get; set; }
        /// <summary>
        /// Gets or Sets Products
        /// </summary>
        [DataMember(Name="products", EmitDefaultValue=false)]
        public List<Products> Products { get; set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Suppliers {\n");
            sb.Append("  SupplierId: ").Append(SupplierId).Append("\n");
            sb.Append("  CompanyName: ").Append(CompanyName).Append("\n");
            sb.Append("  ContactName: ").Append(ContactName).Append("\n");
            sb.Append("  ContactTitle: ").Append(ContactTitle).Append("\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
            sb.Append("  City: ").Append(City).Append("\n");
            sb.Append("  Region: ").Append(Region).Append("\n");
            sb.Append("  PostalCode: ").Append(PostalCode).Append("\n");
            sb.Append("  Country: ").Append(Country).Append("\n");
            sb.Append("  Phone: ").Append(Phone).Append("\n");
            sb.Append("  Fax: ").Append(Fax).Append("\n");
            sb.Append("  HomePage: ").Append(HomePage).Append("\n");
            sb.Append("  Products: ").Append(Products).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as Suppliers);
        }

        /// <summary>
        /// Returns true if Suppliers instances are equal
        /// </summary>
        /// <param name="other">Instance of Suppliers to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Suppliers other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.SupplierId == other.SupplierId ||
                    this.SupplierId != null &&
                    this.SupplierId.Equals(other.SupplierId)
                ) && 
                (
                    this.CompanyName == other.CompanyName ||
                    this.CompanyName != null &&
                    this.CompanyName.Equals(other.CompanyName)
                ) && 
                (
                    this.ContactName == other.ContactName ||
                    this.ContactName != null &&
                    this.ContactName.Equals(other.ContactName)
                ) && 
                (
                    this.ContactTitle == other.ContactTitle ||
                    this.ContactTitle != null &&
                    this.ContactTitle.Equals(other.ContactTitle)
                ) && 
                (
                    this.Address == other.Address ||
                    this.Address != null &&
                    this.Address.Equals(other.Address)
                ) && 
                (
                    this.City == other.City ||
                    this.City != null &&
                    this.City.Equals(other.City)
                ) && 
                (
                    this.Region == other.Region ||
                    this.Region != null &&
                    this.Region.Equals(other.Region)
                ) && 
                (
                    this.PostalCode == other.PostalCode ||
                    this.PostalCode != null &&
                    this.PostalCode.Equals(other.PostalCode)
                ) && 
                (
                    this.Country == other.Country ||
                    this.Country != null &&
                    this.Country.Equals(other.Country)
                ) && 
                (
                    this.Phone == other.Phone ||
                    this.Phone != null &&
                    this.Phone.Equals(other.Phone)
                ) && 
                (
                    this.Fax == other.Fax ||
                    this.Fax != null &&
                    this.Fax.Equals(other.Fax)
                ) && 
                (
                    this.HomePage == other.HomePage ||
                    this.HomePage != null &&
                    this.HomePage.Equals(other.HomePage)
                ) && 
                (
                    this.Products == other.Products ||
                    this.Products != null &&
                    this.Products.SequenceEqual(other.Products)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.SupplierId != null)
                    hash = hash * 59 + this.SupplierId.GetHashCode();
                if (this.CompanyName != null)
                    hash = hash * 59 + this.CompanyName.GetHashCode();
                if (this.ContactName != null)
                    hash = hash * 59 + this.ContactName.GetHashCode();
                if (this.ContactTitle != null)
                    hash = hash * 59 + this.ContactTitle.GetHashCode();
                if (this.Address != null)
                    hash = hash * 59 + this.Address.GetHashCode();
                if (this.City != null)
                    hash = hash * 59 + this.City.GetHashCode();
                if (this.Region != null)
                    hash = hash * 59 + this.Region.GetHashCode();
                if (this.PostalCode != null)
                    hash = hash * 59 + this.PostalCode.GetHashCode();
                if (this.Country != null)
                    hash = hash * 59 + this.Country.GetHashCode();
                if (this.Phone != null)
                    hash = hash * 59 + this.Phone.GetHashCode();
                if (this.Fax != null)
                    hash = hash * 59 + this.Fax.GetHashCode();
                if (this.HomePage != null)
                    hash = hash * 59 + this.HomePage.GetHashCode();
                if (this.Products != null)
                    hash = hash * 59 + this.Products.GetHashCode();
                return hash;
            }
        }
    }

}
