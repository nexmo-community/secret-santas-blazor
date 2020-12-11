# Secret Santa's Blazor

<img src="https://developer.nexmo.com/assets/images/Vonage_Nexmo.svg" height="48px" alt="Nexmo is now known as Vonage" />

## Welcome to Vonage

If you're new to Vonage, you can [sign up for a Vonage API account](https://dashboard.nexmo.com/sign-up?utm_source=DEV_REL&utm_medium=github&utm_campaign=secret-santas-blazor) and get some free credit to get you started.

This repo contains an app that [I built](https://github.com/slorello89) to handle a distributed Secret Santa for my family in 2020, since we couldn't be together because of COVID.

## Perquisites

* You'll need the latest [.NET SDK](https://dotnet.microsoft.com/download)
* You'll need to have a [Postgresql](https://www.postgresql.org/) installed, alternatively you could use whatever database you'd like, you'll just need to update the database context

## Configure the App

Open your `appsettings.json` file and add the following keys to it:

```text
"CONNECTION_STRING": "Host=localhost;Database=secretsantausers;User Id=username;Password=password;Port=5432",
"API_KEY": "API_KEY",
"API_SECRET": "API_SECRET",
"VONAGE_NUMBER": "VONAGE_NUMBER"
```

Set the `API_KEY` and `API_SECRET` with your api key and secret from your [Vonage Dashboard](https://dashboar.nexmo.com/settings). Set the `VONAGE_NUMBER` to one of your [Vonage virtual numbers](https://dashboard.nexmo.com/your-numbers). Set the admin number to the cellphone number of whoever you want to be the admin of the game (presumably yourself). Finally, set the `CONNECTION_STRING` to whatever your database's connection string.

### Migrate the Database

Finally, we'll need to migrate the database. To do this you're going to need the entity framework tool:

```sh
dotnet tool install --global dotnet-ef
```

Then run the fluent tool to create the migration:

```sh
dotnet ef migrations add initial_create 
```

Finally, run the update tool from fluent to apply all the appropriate migrations:

```sh
dotnet ef database update
```

## Getting Help

We love to hear from you so if you have questions, comments or find a bug in the project, let us know! You can either:

* Open an issue on this repository
* Tweet at us! We're [@VonageDev on Twitter](https://twitter.com/VonageDev)
* Or [join the Vonage Developer Community Slack](https://developer.nexmo.com/community/slack)

## Further Reading

* Check out the Developer Documentation at <https://developer.nexmo.com>

<!-- add links to the api reference, other documentation, related blog posts, whatever someone who has read this far might find interesting :) -->

