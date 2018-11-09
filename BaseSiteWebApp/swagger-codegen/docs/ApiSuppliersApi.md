# IO.Swagger.Api.ApiSuppliersApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**Get**](ApiSuppliersApi.md#get) | **GET** /api/ApiSuppliers | 


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
            
            var apiInstance = new ApiSuppliersApi();

            try
            {
                apiInstance.Get();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ApiSuppliersApi.Get: " + e.Message );
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

