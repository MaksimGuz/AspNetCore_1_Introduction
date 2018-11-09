# IO.Swagger.Api.ApiCategoryImagesApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**Get**](ApiCategoryImagesApi.md#get) | **GET** /api/ApiCategoryImages/{id} | 


<a name="get"></a>
# **Get**
> void Get (int? id)



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
            
            var apiInstance = new ApiCategoryImagesApi();
            var id = 56;  // int? | 

            try
            {
                apiInstance.Get(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ApiCategoryImagesApi.Get: " + e.Message );
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

