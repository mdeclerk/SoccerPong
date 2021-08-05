# SoccerPONG

My very own implementation of the classic Atari Pong Game to explore the development process of a Full-stack Web App using ASP.NET Blazor WebAssembly.

![SoccerPong](SoccerPong.gif)

## Play Online

> ## mdeclerk.github.io/SoccerPong

_To setup GitHub pages I followed [this](https://swimburger.net/blog/dotnet/how-to-deploy-aspnet-blazor-webassembly-to-github-pages) awesome tutorial._

## Play Locally

1. Install [Microsoft .NET SDK 5.0](https://dotnet.microsoft.com/download)

2. Clone Project from GitHub

3. Start Host from CLI (Navigate to project root folder)

    `$ dotnet run`

4. Play in Browser: http://localhost:5000

## Code Layout

- **Views/** contains front-end UI using Blazor components
    - **Views/Components/** contains reusable UI components
    - **Views/Pages/** contains routable pages

- **Services/** contains back-end game related objects implemented as ASP.NET services managed by dependency injection system

- **wwwroot/** contains static web files e.g. css, images, and [bootstrap5](https://getbootstrap.com/) front-end toolkit