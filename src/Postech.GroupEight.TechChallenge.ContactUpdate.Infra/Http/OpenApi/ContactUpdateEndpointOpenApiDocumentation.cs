using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Enumerators;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.OpenApi.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.OpenApi;

[ExcludeFromCodeCoverage]
public record ContactUpdateEndpointOpenApiDocumentation : IEndpointOpenApiDocumentation
{
    private static readonly Guid _contactId = Guid.NewGuid();

    public OpenApiOperation GetOpenApiDocumentation()
    {
        return new()
        {
            Summary = "Requests an update from a specific contact.",
            Description = "This endpoint requests the update of data for a previously registered contact for the persistence microservice.",
            Parameters = GetParametersDocumentation(),
            RequestBody = GetRequestBodyDocumentation(),
            Responses = GetResponsesDocumentation(),
            Tags = [new() { Name = "/contacts"}]
        };
    }

    private static OpenApiResponses GetResponsesDocumentation()
    {
        return new OpenApiResponses
        {
            ["202"] = new OpenApiResponse
            {
                Description = "Contact update request successfully sent to persistence microservice.",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Description = "Data describing the outcome of a contact update request.",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["messages"] = new OpenApiSchema
                                {
                                    Type = "array",
                                    Description = "Notifications about the contact update request process.",
                                    Nullable = true,
                                    Items = new OpenApiSchema
                                    {
                                        Type = "object",
                                        Description = "Notification about the contact update request process.",
                                        Properties = new Dictionary<string, OpenApiSchema>
                                        {
                                            ["message"] = new OpenApiSchema
                                            {
                                                Type = "string",
                                                Description = "The notification content.",
                                                Example = new OpenApiString("Contact update request sent successfully.") 
                                            },
                                            ["type"] = new OpenApiSchema
                                            {
                                                Type = "integer",
                                                Description = "The identification of the type of notification.\n0: Info\n1: Warning\n2: Error\n3: Critical",
                                                Example = new OpenApiInteger(NotificationType.Info.GetHashCode()),
                                                Enum = [
                                                    new OpenApiInteger(NotificationType.Info.GetHashCode()),
                                                    new OpenApiInteger(NotificationType.Warning.GetHashCode()),
                                                    new OpenApiInteger(NotificationType.Error.GetHashCode()),
                                                    new OpenApiInteger(NotificationType.Critical.GetHashCode())
                                                ]
                                            }
                                        }
                                    },                                  
                                },
                                ["data"] = new OpenApiSchema
                                {
                                    Type = "object",
                                    Description = "Data from the contact update request.",
                                    Nullable = true,
                                    Properties = new Dictionary<string, OpenApiSchema>
                                    {
                                        ["contactId"] = new OpenApiSchema
                                        {
                                            Type = "string",
                                            Format = "uuid",
                                            Description = "The unique identifier of the contact sent for update.",
                                            Example = new OpenApiString(_contactId.ToString()) 
                                        },
                                        ["isContactNotifiedForUpdate"] = new OpenApiSchema
                                        {
                                            Type = "boolean",
                                            Description = "Indicates whether the contact was sent for update.",
                                            Example = new OpenApiBoolean(true)
                                        },
                                        ["contactNotifiedForUpdateAt"] = new OpenApiSchema
                                        {
                                            Type = "string",
                                            Format = "date-time",
                                            Description = "Date and time the contact was sent for update.",
                                            Example = new OpenApiString(DateTime.UtcNow.ToString())
                                        }
                                    }
                                },
                                ["isSuccessfullyProcessed"] = new OpenApiSchema
                                {
                                    Type = "boolean",
                                    Description = "Indicates whether the contact update request was processed successfully.",
                                    Example = new OpenApiBoolean(true)
                                }
                            }
                        }
                    }
                }
            },
            ["400"] = new OpenApiResponse
            {
                Description = "The request was sent with invalid data that did not allow the contact update request.",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Description = "Data describing the outcome of a contact update request.",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["messages"] = new OpenApiSchema
                                {
                                    Type = "array",
                                    Description = "Notifications about errors identified during the contact update request.",
                                    Nullable = true,
                                    Items = new OpenApiSchema
                                    {
                                        Type = "object",
                                        Description = "Notification about errors identified during the contact update request.",
                                        Properties = new Dictionary<string, OpenApiSchema>
                                        {
                                            ["message"] = new OpenApiSchema
                                            {
                                                Type = "string",
                                                Description = "The notification content.",
                                                Example = new OpenApiString("The contact update request was not sent.") 
                                            },
                                            ["type"] = new OpenApiSchema
                                            {
                                                Type = "integer",
                                                Description = "The identification of the type of notification.\n0: Info\n1: Warning\n2: Error\n3: Critical",
                                                Example = new OpenApiInteger(NotificationType.Error.GetHashCode()),
                                                Enum = [
                                                    new OpenApiInteger(NotificationType.Info.GetHashCode()),
                                                    new OpenApiInteger(NotificationType.Warning.GetHashCode()),
                                                    new OpenApiInteger(NotificationType.Error.GetHashCode()),
                                                    new OpenApiInteger(NotificationType.Critical.GetHashCode())
                                                ]
                                            }
                                        }
                                    },                                  
                                },
                                ["data"] = new OpenApiSchema
                                {
                                    Type = "object",
                                    Description = "Data from the contact update request.",
                                    Nullable = true,
                                    Properties = new Dictionary<string, OpenApiSchema>
                                    {
                                        ["contactId"] = new OpenApiSchema
                                        {
                                            Type = "string",
                                            Format = "uuid",
                                            Description = "The unique identifier of the contact sent for update.",
                                            Example = new OpenApiString(null)
                                        },
                                        ["isContactNotifiedForUpdate"] = new OpenApiSchema
                                        {
                                            Type = "boolean",
                                            Description = "Indicates whether the contact was sent for update.",
                                            Example = new OpenApiString(null)
                                        },
                                        ["contactNotifiedForUpdateAt"] = new OpenApiSchema
                                        {
                                            Type = "string",
                                            Format = "date-time",
                                            Description = "Date and time the contact was sent for update.",
                                            Example = new OpenApiBoolean(false)
                                        }
                                    },
                                    Example = null
                                },
                                ["isSuccessfullyProcessed"] = new OpenApiSchema
                                {
                                    Type = "boolean",
                                    Description = "Indicates whether the contact update request was processed successfully.",
                                    Example = new OpenApiBoolean(false)
                                }
                            }
                        }
                    }
                }
            },
        };
    }

    private static OpenApiRequestBody GetRequestBodyDocumentation()
    {
        return new OpenApiRequestBody
        {
            Required = true,
            Description = "Data required to request a contact update.",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Required = new HashSet<string> { "contactId", "currentContactData", "updatedContactData" },
                        Properties = new Dictionary<string, OpenApiSchema>
                        {
                            ["contactId"] = new OpenApiSchema()
                            {
                                Type = "string",
                                Format = "uuid",
                                Description = "Identifier of the contact whose update request will be sent to.",
                                Example = new OpenApiString(_contactId.ToString()),
                            },
                            ["currentContactData"] = new OpenApiSchema()
                            {
                                Type = "object",
                                Description = "The data currently registered for the contact.",
                                Required = new HashSet<string> { "contactName", "contactEmail", "contactPhone" },
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["contactName"] = new OpenApiSchema()
                                    {
                                        Type = "object",
                                        Description = "The name data currently registered for the contact.",
                                        Required = new HashSet<string> { "firstName", "lastName" },
                                        Properties = new Dictionary<string, OpenApiSchema>
                                        {
                                            ["firstName"] = new OpenApiSchema()
                                            {
                                                Type = "string",
                                                Description = "The first name currently registered for the contact.",
                                                Example = new OpenApiString("Rodrigo")
                                            },
                                            ["lastName"] = new OpenApiSchema()
                                            {
                                                Type = "string",
                                                Description = "The last name currently registered for the contact.",
                                                Example = new OpenApiString("Herrera")
                                            }
                                        }
                                    },
                                    ["contactEmail"] = new OpenApiSchema()
                                    {
                                        Type = "object",
                                        Description = "The e-mail data currently registered for the contact.",
                                        Required = new HashSet<string> { "address" },
                                        Properties = new Dictionary<string, OpenApiSchema>
                                        {
                                            ["address"] = new OpenApiSchema()
                                            {
                                                Type = "string",
                                                Description = "The e-mail address currently registered for the contact.",
                                                Example = new OpenApiString("rodrigo.herrera@gmail.com")
                                            }
                                        }
                                    },
                                    ["contactPhone"] = new OpenApiSchema()
                                    {
                                        Type = "object",
                                        Description = "The phone data currently registered for the contact.",
                                        Required = new HashSet<string> { "number", "areaCode" },
                                        Properties = new Dictionary<string, OpenApiSchema>
                                        {
                                            ["number"] = new OpenApiSchema()
                                            {
                                                Type = "string",
                                                Description = "The phone number currently registered for the contact.",
                                                Example = new OpenApiString("984326218")
                                            },
                                            ["areaCode"] = new OpenApiSchema()
                                            {
                                                Type = "string",
                                                Description = "The phone number area code currently registered for the contact.",
                                                Example = new OpenApiString("11")
                                            }
                                        }
                                    }
                                }
                            },
                            ["updatedContactData"] = new OpenApiSchema()
                            {
                                Type = "object",
                                Description = "The update details for the contact.",
                                Required = new HashSet<string> { "contactName", "contactEmail", "contactPhone" },
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["contactName"] = new OpenApiSchema()
                                    {
                                        Type = "object",
                                        Description = "The new name data provided for the contact.",
                                        Required = new HashSet<string> { "firstName", "lastName" },
                                        Properties = new Dictionary<string, OpenApiSchema>
                                        {
                                            ["firstName"] = new OpenApiSchema()
                                            {
                                                Type = "string",
                                                Description = "The new first name provided for the contact.",
                                                Example = new OpenApiString("Rodrigo")
                                            },
                                            ["lastName"] = new OpenApiSchema()
                                            {
                                                Type = "string",
                                                Description = "The new last name provided for the contact.",
                                                Example = new OpenApiString("Garro")
                                            }
                                        }
                                    },
                                    ["contactEmail"] = new OpenApiSchema()
                                    {
                                        Type = "object",
                                        Description = "The new e-mail data provided for the contact.",
                                        Required = new HashSet<string> { "address" },
                                        Properties = new Dictionary<string, OpenApiSchema>
                                        {
                                            ["address"] = new OpenApiSchema()
                                            {
                                                Type = "string",
                                                Description = "The new e-mail address provided for the contact.",
                                                Example = new OpenApiString("rodrigo.garro@gmail.com")
                                            }
                                        }
                                    },
                                    ["contactPhone"] = new OpenApiSchema()
                                    {
                                        Type = "object",
                                        Description = "The new phone data provided for the contact.",
                                        Required = new HashSet<string> { "number", "areaCode" },
                                        Properties = new Dictionary<string, OpenApiSchema>
                                        {
                                            ["number"] = new OpenApiSchema()
                                            {
                                                Type = "string",
                                                Description = "The new phone number provided for the contact.",
                                                Example = new OpenApiString("946719251")
                                            },
                                            ["areaCode"] = new OpenApiSchema()
                                            {
                                                Type = "string",
                                                Description = "The new phone number area code provided for the contact.",
                                                Example = new OpenApiString("11")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
    }

    private static IList<OpenApiParameter> GetParametersDocumentation()
    {
        return 
        [
            new()
            {
                Name = "contactId",
                In = ParameterLocation.Path,
                Required = true,
                Description = "Identifier of the contact whose update request will be sent to.",
                Schema = new OpenApiSchema { Type = "string", Format = "uuid"  }
            }
        ];
    }
}