# IO.Swagger.Api.ApiProductsApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**Create**](ApiProductsApi.md#create) | **POST** /api/ApiProducts | 
[**Delete**](ApiProductsApi.md#delete) | **DELETE** /api/ApiProducts/{id} | 
[**Edit**](ApiProductsApi.md#edit) | **PUT** /api/ApiProducts/{id} | 
[**Get**](ApiProductsApi.md#get) | **GET** /api/ApiProducts | 
[**Get_0**](ApiProductsApi.md#get_0) | **GET** /api/ApiProducts/{id} | 


<a name="create"></a>
# **Create**
> void Create (string productName, string categoryCategoryName, int? productId = null, int? supplierId = null, int? categoryId = null, string quantityPerUnit = null, double? unitPrice = null, int? unitsInStock = null, int? unitsOnOrder = null, int? reorderLevel = null, bool? discontinued = null, int? categoryCategoryId = null, string categoryDescription = null, byte[] categoryPicture = null, List<string> categoryProducts = null, int? supplierSupplierId = null, string supplierCompanyName = null, string supplierContactName = null, string supplierContactTitle = null, string supplierAddress = null, string supplierCity = null, string supplierRegion = null, string supplierPostalCode = null, string supplierCountry = null, string supplierPhone = null, string supplierFax = null, string supplierHomePage = null, List<string> supplierProducts = null, List<string> orderDetails = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CreateExample
    {
        public void main()
        {
            
            var apiInstance = new ApiProductsApi();
            var productName = productName_example;  // string | 
            var categoryCategoryName = categoryCategoryName_example;  // string | 
            var productId = 56;  // int? |  (optional) 
            var supplierId = 56;  // int? |  (optional) 
            var categoryId = 56;  // int? |  (optional) 
            var quantityPerUnit = quantityPerUnit_example;  // string |  (optional) 
            var unitPrice = 1.2;  // double? |  (optional) 
            var unitsInStock = 56;  // int? |  (optional) 
            var unitsOnOrder = 56;  // int? |  (optional) 
            var reorderLevel = 56;  // int? |  (optional) 
            var discontinued = true;  // bool? |  (optional) 
            var categoryCategoryId = 56;  // int? |  (optional) 
            var categoryDescription = categoryDescription_example;  // string |  (optional) 
            var categoryPicture = B;  // byte[] |  (optional) 
            var categoryProducts = new List<string>(); // List<string> |  (optional) 
            var supplierSupplierId = 56;  // int? |  (optional) 
            var supplierCompanyName = supplierCompanyName_example;  // string |  (optional) 
            var supplierContactName = supplierContactName_example;  // string |  (optional) 
            var supplierContactTitle = supplierContactTitle_example;  // string |  (optional) 
            var supplierAddress = supplierAddress_example;  // string |  (optional) 
            var supplierCity = supplierCity_example;  // string |  (optional) 
            var supplierRegion = supplierRegion_example;  // string |  (optional) 
            var supplierPostalCode = supplierPostalCode_example;  // string |  (optional) 
            var supplierCountry = supplierCountry_example;  // string |  (optional) 
            var supplierPhone = supplierPhone_example;  // string |  (optional) 
            var supplierFax = supplierFax_example;  // string |  (optional) 
            var supplierHomePage = supplierHomePage_example;  // string |  (optional) 
            var supplierProducts = new List<string>(); // List<string> |  (optional) 
            var orderDetails = new List<string>(); // List<string> |  (optional) 

            try
            {
                apiInstance.Create(productName, categoryCategoryName, productId, supplierId, categoryId, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued, categoryCategoryId, categoryDescription, categoryPicture, categoryProducts, supplierSupplierId, supplierCompanyName, supplierContactName, supplierContactTitle, supplierAddress, supplierCity, supplierRegion, supplierPostalCode, supplierCountry, supplierPhone, supplierFax, supplierHomePage, supplierProducts, orderDetails);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ApiProductsApi.Create: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **productName** | **string**|  | 
 **categoryCategoryName** | **string**|  | 
 **productId** | **int?**|  | [optional] 
 **supplierId** | **int?**|  | [optional] 
 **categoryId** | **int?**|  | [optional] 
 **quantityPerUnit** | **string**|  | [optional] 
 **unitPrice** | **double?**|  | [optional] 
 **unitsInStock** | **int?**|  | [optional] 
 **unitsOnOrder** | **int?**|  | [optional] 
 **reorderLevel** | **int?**|  | [optional] 
 **discontinued** | **bool?**|  | [optional] 
 **categoryCategoryId** | **int?**|  | [optional] 
 **categoryDescription** | **string**|  | [optional] 
 **categoryPicture** | **byte[]**|  | [optional] 
 **categoryProducts** | [**List<string>**](string.md)|  | [optional] 
 **supplierSupplierId** | **int?**|  | [optional] 
 **supplierCompanyName** | **string**|  | [optional] 
 **supplierContactName** | **string**|  | [optional] 
 **supplierContactTitle** | **string**|  | [optional] 
 **supplierAddress** | **string**|  | [optional] 
 **supplierCity** | **string**|  | [optional] 
 **supplierRegion** | **string**|  | [optional] 
 **supplierPostalCode** | **string**|  | [optional] 
 **supplierCountry** | **string**|  | [optional] 
 **supplierPhone** | **string**|  | [optional] 
 **supplierFax** | **string**|  | [optional] 
 **supplierHomePage** | **string**|  | [optional] 
 **supplierProducts** | [**List<string>**](string.md)|  | [optional] 
 **orderDetails** | [**List<string>**](string.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="delete"></a>
# **Delete**
> void Delete (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class DeleteExample
    {
        public void main()
        {
            
            var apiInstance = new ApiProductsApi();
            var id = 56;  // int? | 

            try
            {
                apiInstance.Delete(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ApiProductsApi.Delete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="edit"></a>
# **Edit**
> void Edit (int? id, string productName, string categoryCategoryName, int? productId = null, int? supplierId = null, int? categoryId = null, string quantityPerUnit = null, double? unitPrice = null, int? unitsInStock = null, int? unitsOnOrder = null, int? reorderLevel = null, bool? discontinued = null, int? categoryCategoryId = null, string categoryDescription = null, byte[] categoryPicture = null, List<string> categoryProducts = null, int? supplierSupplierId = null, string supplierCompanyName = null, string supplierContactName = null, string supplierContactTitle = null, string supplierAddress = null, string supplierCity = null, string supplierRegion = null, string supplierPostalCode = null, string supplierCountry = null, string supplierPhone = null, string supplierFax = null, string supplierHomePage = null, List<string> supplierProducts = null, List<string> orderDetails = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class EditExample
    {
        public void main()
        {
            
            var apiInstance = new ApiProductsApi();
            var id = 56;  // int? | 
            var productName = productName_example;  // string | 
            var categoryCategoryName = categoryCategoryName_example;  // string | 
            var productId = 56;  // int? |  (optional) 
            var supplierId = 56;  // int? |  (optional) 
            var categoryId = 56;  // int? |  (optional) 
            var quantityPerUnit = quantityPerUnit_example;  // string |  (optional) 
            var unitPrice = 1.2;  // double? |  (optional) 
            var unitsInStock = 56;  // int? |  (optional) 
            var unitsOnOrder = 56;  // int? |  (optional) 
            var reorderLevel = 56;  // int? |  (optional) 
            var discontinued = true;  // bool? |  (optional) 
            var categoryCategoryId = 56;  // int? |  (optional) 
            var categoryDescription = categoryDescription_example;  // string |  (optional) 
            var categoryPicture = B;  // byte[] |  (optional) 
            var categoryProducts = new List<string>(); // List<string> |  (optional) 
            var supplierSupplierId = 56;  // int? |  (optional) 
            var supplierCompanyName = supplierCompanyName_example;  // string |  (optional) 
            var supplierContactName = supplierContactName_example;  // string |  (optional) 
            var supplierContactTitle = supplierContactTitle_example;  // string |  (optional) 
            var supplierAddress = supplierAddress_example;  // string |  (optional) 
            var supplierCity = supplierCity_example;  // string |  (optional) 
            var supplierRegion = supplierRegion_example;  // string |  (optional) 
            var supplierPostalCode = supplierPostalCode_example;  // string |  (optional) 
            var supplierCountry = supplierCountry_example;  // string |  (optional) 
            var supplierPhone = supplierPhone_example;  // string |  (optional) 
            var supplierFax = supplierFax_example;  // string |  (optional) 
            var supplierHomePage = supplierHomePage_example;  // string |  (optional) 
            var supplierProducts = new List<string>(); // List<string> |  (optional) 
            var orderDetails = new List<string>(); // List<string> |  (optional) 

            try
            {
                apiInstance.Edit(id, productName, categoryCategoryName, productId, supplierId, categoryId, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued, categoryCategoryId, categoryDescription, categoryPicture, categoryProducts, supplierSupplierId, supplierCompanyName, supplierContactName, supplierContactTitle, supplierAddress, supplierCity, supplierRegion, supplierPostalCode, supplierCountry, supplierPhone, supplierFax, supplierHomePage, supplierProducts, orderDetails);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ApiProductsApi.Edit: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 
 **productName** | **string**|  | 
 **categoryCategoryName** | **string**|  | 
 **productId** | **int?**|  | [optional] 
 **supplierId** | **int?**|  | [optional] 
 **categoryId** | **int?**|  | [optional] 
 **quantityPerUnit** | **string**|  | [optional] 
 **unitPrice** | **double?**|  | [optional] 
 **unitsInStock** | **int?**|  | [optional] 
 **unitsOnOrder** | **int?**|  | [optional] 
 **reorderLevel** | **int?**|  | [optional] 
 **discontinued** | **bool?**|  | [optional] 
 **categoryCategoryId** | **int?**|  | [optional] 
 **categoryDescription** | **string**|  | [optional] 
 **categoryPicture** | **byte[]**|  | [optional] 
 **categoryProducts** | [**List<string>**](string.md)|  | [optional] 
 **supplierSupplierId** | **int?**|  | [optional] 
 **supplierCompanyName** | **string**|  | [optional] 
 **supplierContactName** | **string**|  | [optional] 
 **supplierContactTitle** | **string**|  | [optional] 
 **supplierAddress** | **string**|  | [optional] 
 **supplierCity** | **string**|  | [optional] 
 **supplierRegion** | **string**|  | [optional] 
 **supplierPostalCode** | **string**|  | [optional] 
 **supplierCountry** | **string**|  | [optional] 
 **supplierPhone** | **string**|  | [optional] 
 **supplierFax** | **string**|  | [optional] 
 **supplierHomePage** | **string**|  | [optional] 
 **supplierProducts** | [**List<string>**](string.md)|  | [optional] 
 **orderDetails** | [**List<string>**](string.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="get"></a>
# **Get**
> void Get ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetExample
    {
        public void main()
        {
            
            var apiInstance = new ApiProductsApi();

            try
            {
                apiInstance.Get();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ApiProductsApi.Get: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="get_0"></a>
# **Get_0**
> void Get_0 (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class Get_0Example
    {
        public void main()
        {
            
            var apiInstance = new ApiProductsApi();
            var id = 56;  // int? | 

            try
            {
                apiInstance.Get_0(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ApiProductsApi.Get_0: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

