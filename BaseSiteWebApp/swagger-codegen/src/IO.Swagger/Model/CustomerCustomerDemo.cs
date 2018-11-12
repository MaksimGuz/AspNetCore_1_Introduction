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
    /// CustomerCustomerDemo
    /// </summary>
    [DataContract]
    public partial class CustomerCustomerDemo :  IEquatable<CustomerCustomerDemo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerCustomerDemo" /> class.
        /// </summary>
        /// <param name="CustomerId">CustomerId.</param>
        /// <param name="CustomerTypeId">CustomerTypeId.</param>
        /// <param name="Customer">Customer.</param>
        /// <param name="CustomerType">CustomerType.</param>
        public CustomerCustomerDemo(string CustomerId = null, string CustomerTypeId = null, Customers Customer = null, CustomerDemographics CustomerType = null)
        {
            this.CustomerId = CustomerId;
            this.CustomerTypeId = CustomerTypeId;
            this.Customer = Customer;
            this.CustomerType = CustomerType;
        }
        
        /// <summary>
        /// Gets or Sets CustomerId
        /// </summary>
        [DataMember(Name="customerId", EmitDefaultValue=false)]
        public string CustomerId { get; set; }
        /// <summary>
        /// Gets or Sets CustomerTypeId
        /// </summary>
        [DataMember(Name="customerTypeId", EmitDefaultValue=false)]
        public string CustomerTypeId { get; set; }
        /// <summary>
        /// Gets or Sets Customer
        /// </summary>
        [DataMember(Name="customer", EmitDefaultValue=false)]
        public Customers Customer { get; set; }
        /// <summary>
        /// Gets or Sets CustomerType
        /// </summary>
        [DataMember(Name="customerType", EmitDefaultValue=false)]
        public CustomerDemographics CustomerType { get; set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CustomerCustomerDemo {\n");
            sb.Append("  CustomerId: ").Append(CustomerId).Append("\n");
            sb.Append("  CustomerTypeId: ").Append(CustomerTypeId).Append("\n");
            sb.Append("  Customer: ").Append(Customer).Append("\n");
            sb.Append("  CustomerType: ").Append(CustomerType).Append("\n");
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
            return this.Equals(obj as CustomerCustomerDemo);
        }

        /// <summary>
        /// Returns true if CustomerCustomerDemo instances are equal
        /// </summary>
        /// <param name="other">Instance of CustomerCustomerDemo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CustomerCustomerDemo other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.CustomerId == other.CustomerId ||
                    this.CustomerId != null &&
                    this.CustomerId.Equals(other.CustomerId)
                ) && 
                (
                    this.CustomerTypeId == other.CustomerTypeId ||
                    this.CustomerTypeId != null &&
                    this.CustomerTypeId.Equals(other.CustomerTypeId)
                ) && 
                (
                    this.Customer == other.Customer ||
                    this.Customer != null &&
                    this.Customer.Equals(other.Customer)
                ) && 
                (
                    this.CustomerType == other.CustomerType ||
                    this.CustomerType != null &&
                    this.CustomerType.Equals(other.CustomerType)
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
                if (this.CustomerId != null)
                    hash = hash * 59 + this.CustomerId.GetHashCode();
                if (this.CustomerTypeId != null)
                    hash = hash * 59 + this.CustomerTypeId.GetHashCode();
                if (this.Customer != null)
                    hash = hash * 59 + this.Customer.GetHashCode();
                if (this.CustomerType != null)
                    hash = hash * 59 + this.CustomerType.GetHashCode();
                return hash;
            }
        }
    }

}