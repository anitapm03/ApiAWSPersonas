using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using ApiAWSPersonas.Models;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ApiAWSPersonas;

public class Functions
{
    public List<Persona> personasList;
    public Functions()
    {
        this.personasList = new List<Persona>();
        Persona p = new Persona
        {
            IdPersona = 1,
            Nombre = "Marta",
            Email = "m@gmail.com",
            Edad = 22
        };
        this.personasList.Add(p);
        Persona p2 = new Persona
        {
            IdPersona = 2,
            Nombre = "Gabriel",
            Email = "g@gmail.com",
            Edad = 18
        };
        this.personasList.Add(p2);
        Persona p3 = new Persona
        {
            IdPersona = 3,
            Nombre = "Ana",
            Email = "a@gmail.com",
            Edad = 20
        };
        this.personasList.Add(p3);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/")]
    public IHttpResult Get(ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        string json = JsonConvert.SerializeObject(this.personasList);
        return HttpResults.Ok(json);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/find/{id}")]
    public IHttpResult Find(int id, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        Persona persona = this.personasList[id];
        string json = JsonConvert.SerializeObject(persona);
        return HttpResults.Ok(json);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Post, "/post")]
    public IHttpResult Post
        ([FromBody]Persona persona, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'POST' Request");
        string json = JsonConvert.SerializeObject(persona);
        return HttpResults.Ok(json);
    }
}
