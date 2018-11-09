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
    /// Categories
    /// </summary>
    [DataContract]
    public partial class Categories :  IEquatable<Categories>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Categories" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Categories() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Categories" /> class.
        /// </summary>
        /// <param name="CategoryId">CategoryId.</param>
        /// <param name="CategoryName">CategoryName (required).</param>
        /// <param name="Description">Description.</param>
        /// <param name="Picture">Picture.</param>
        /// <param name="Products">Products.</param>
        public Categories(int? CategoryId = null, string CategoryName = null, string Description = null, byte[] Picture = null, List<Products> Products = null)
        {
            // to ensure "CategoryName" is required (not null)
            if (CategoryName == null)
            {
                throw new InvalidDataException("CategoryName is a required property for Categories and cannot be null");
            }
            else
            {
                this.CategoryName = CategoryName;
            }
            this.CategoryId = CategoryId;
            this.Description = Description;
            this.Picture = Picture;
            this.Products = Products;
        }
        
        /// <summary>
        /// Gets or Sets CategoryId
        /// </summary>
        [DataMember(Name="categoryId", EmitDefaultValue=false)]
        public int? CategoryId { get; set; }
        /// <summary>
        /// Gets or Sets CategoryName
        /// </summary>
        [DataMember(Name="categoryName", EmitDefaultValue=false)]
        public string CategoryName { get; set; }
        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }
        /// <summary>
        /// Gets or Sets Picture
        /// </summary>
        [DataMember(Name="picture", EmitDefaultValue=false)]
        public byte[] Picture { get; set; }
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
            sb.Append("class Categories {\n");
            sb.Append("  CategoryId: ").Append(CategoryId).Append("\n");
            sb.Append("  CategoryName: ").Append(CategoryName).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Picture: ").Append(Picture).Append("\n");
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
            return this.Equals(obj as Categories);
        }

        /// <summary>
        /// Returns true if Categories instances are equal
        /// </summary>
        /// <param name="other">Instance of Categories to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Categories other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.CategoryId == other.CategoryId ||
                    this.CategoryId != null &&
                    this.CategoryId.Equals(other.CategoryId)
                ) && 
                (
                    this.CategoryName == other.CategoryName ||
                    this.CategoryName != null &&
                    this.CategoryName.Equals(other.CategoryName)
                ) && 
                (
                    this.Description == other.Description ||
                    this.Description != null &&
                    this.Description.Equals(other.Description)
                ) && 
                (
                    this.Picture == other.Picture ||
                    this.Picture != null &&
                    this.Picture.Equals(other.Picture)
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
                if (this.CategoryId != null)
                    hash = hash * 59 + this.CategoryId.GetHashCode();
                if (this.CategoryName != null)
                    hash = hash * 59 + this.CategoryName.GetHashCode();
                if (this.Description != null)
                    hash = hash * 59 + this.Description.GetHashCode();
                if (this.Picture != null)
                    hash = hash * 59 + this.Picture.GetHashCode();
                if (this.Products != null)
                    hash = hash * 59 + this.Products.GetHashCode();
                return hash;
            }
        }
    }

}
