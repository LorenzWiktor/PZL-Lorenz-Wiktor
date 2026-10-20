# PZL-Lorenz-Wiktor

Testowa aplikacja ASP.NET Core przygotowana w ramach laboratorium z Programowania zaawansowanego.

## Cel projektu

Sprawdzenie środowiska ASP.NET Core i nauka pracy z Git.

## Technologie

- C#
- .NET
- ASP.NET Core
- Git

## Wymagania środowiskowe

- .NET SDK: .NET SDK 10.0.x — projekt uruchomiono z wersją 10.0.402.
- IDE: Visual Studio Code.

## Uruchomienie

```powershell
dotnet run --project src/PZL.TestApi --launch-profile http
```

Otwórz w przeglądarce http://localhost:5114/.

Endpoint GET / zwraca tekst „API działa.”.

## Autorzy

- Wiktor Lorenz

## Organizacja pracy

Zmiany są przygotowywane na gałęziach roboczych i zapisywane w logicznych commitach.

## Status

Aplikacja działa lokalnie. Sprawdzono odpowiedź HTTP 200 i tekst „API działa.”.

## Znane ograniczenia

- Projekt jest na etapie przygotowania środowiska.
- Jeden endpoint testowy.
- Brak bazy danych i operacji CRUD.
- Lokalne uruchomienie przez HTTP.
