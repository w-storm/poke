# Pokemon Api!

This project demonostrates use and mix of third party RestApis in an .Net Web Api project (C#)


# Structure

**Poke.Api** is the web api project with one controller exposing two GET endpoints as per requirements. It is configured for DI but project exception handling is NOT setup to save time. In production any exceptions should be intercepted, logged and a message returned to the user without internal details.

**Poke.Api.Tests** is a set of tests for the API controller
**Poke.Models** a library of shared models
**Poke.Services** is the service layer with:

 - **ExternalServices** Api Clients for external APIs Pokemon and FunTranslations. To save time and limit of what can go wrong, any exception within the clients will be catched and transformed into its custom exception. In production it will need much more info to properly handle timeouts, not found and etc.
 - **Mappers** is a simple extension to map external client API model to our internal model.
 - **Translators** is a set of translator classes that provide a way to check if any of them can translate based on defined conditions and translate. It provides an easy way to add/remove translators witout major refactoring.
 - **PokemonService** is where we call the third-party API clients and process the translations. As per requirement the Translate method wraps the translation part into a TRY/CATCH to provide the original description if the translation fails for any reason. 
**Poke.Services.Tests** a set of tests for all key classes and extensions. The tests for the API clients simplified but gives an idea how they could be tested.


## Docker Image

If you wish to run it from Docker, open Powershell and pull the image using:
*docker pull westernstorm/pokeapi*
once downloaded, run *docker run -p 4200:80 0a03136024b3*
open url http://localhost:4200/pokemon/mewtwo
and you should see JSON of the pokemon data
open http://localhost:4200/pokemon/translated/mewtwo 
and you will see its yoda translated version

## Visual Studio
To run it from VS you will need at least VS community edition 2019. Pull the latest version of the project from this repo, open solution in the VS and click Debug with IIS Express. A browser window will open and you will be able to test the endpoints as per above.

Sergey