# IO.Swagger.Api.ApiCategoriesApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**Get**](ApiCategoriesApi.md#get) | **GET** /api/ApiCategories | 
[**Get_0**](ApiCategoriesApi.md#get_0) | **GET** /api/ApiCategories/{id} | 
[**Update**](ApiCategoriesApi.md#update) | **PUT** /api/ApiCategories/{id} | 


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
            
            var apiInstance = new ApiCategoriesApi();

            try
            {
                apiInstance.Get();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ApiCategoriesApi.Get: " + e.Message );
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
            
            var apiInstance = new ApiCategoriesApi();
            var id = 56;  // int? | 

            try
            {
                apiInstance.Get_0(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ApiCategoriesApi.Get_0: " + e.Message );
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

<a name="update"></a>
# **Update**
> void Update (int? id, string categoryName, int? categoryId = null, System.IO.Stream pictureFile = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class UpdateExample
    {
        public void main()
        {
            
            var apiInstance = new ApiCategoriesApi();
            var id = 56;  // int? | 
            var categoryName = categoryName_example;  // string | 
            var categoryId = 56;  // int? |  (optional) 
            var pictureFile = new System.IO.Stream(); // System.IO.Stream |  (optional) 

            try
            {
                apiInstance.Update(id, categoryName, categoryId, pictureFile);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ApiCategoriesApi.Update: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 
 **categoryName** | **string**|  | 
 **categoryId** | **int?**|  | [optional] 
 **pictureFile** | **System.IO.Stream**|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

