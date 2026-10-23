# PZL-Lorenz-Wiktor

Testowa aplikacja ASP.NET Core przygotowana w ramach laboratorium z Programowania zaawansowanego.

## Cel projektu

- Sprawdzenie środowiska ASP.NET Core, nauka pracy z Git i obsługi bazy danych.

## Technologie

- C#
- .NET 10
- ASP.NET Core
- Entity Framework Core 10
- SQLite
- Git

## Wymagania środowiskowe

- .NET SDK: .NET SDK 10.0.x — projekt uruchomiono z wersją 10.0.402.
- IDE: Visual Studio Code.

Plik `global.json` wybiera najnowsze zainstalowane stabilne SDK z linii 10.0.x.

## Uruchomienie

Przed pierwszym uruchomieniem wykonaj w głównym katalogu repozytorium:

```powershell
dotnet tool restore
dotnet restore
dotnet ef database update --project src/PZL.TestApi --startup-project src/PZL.TestApi
```

Połączenie jest zapisane w `appsettings.json` jako `DefaultConnection`. Baza SQLite znajduje się w pliku `src/PZL.TestApi/taskflow.db`. Plik bazy jest ignorowany przez Git, a jej strukturę odtwarzają migracje.

Uruchomienie API:

```powershell
dotnet run --project src/PZL.TestApi --launch-profile http
```

Otwórz w przeglądarce http://localhost:5114/.

Endpoint GET / zwraca tekst „API działa.”.

Demonstracja operacji CRUD i zapytań LINQ:

```powershell
dotnet run --project src/PZL.TestApi --launch-profile http -- --demo=crud
```

Demonstracja tworzy projekt, dwa zadania i komentarz. Odczytuje dane, zmienia status pierwszego zadania, a następnie usuwa komentarz i pierwsze zadanie. Projekt i drugie zadanie pozostają w bazie. Każde uruchomienie tworzy nowy projekt demonstracyjny.

Ponowny odczyt zapisanego projektu:

```powershell
dotnet run --project src/PZL.TestApi --launch-profile http --no-build -- --demo=read --project-id=1
```

Wartość `1` zastąp identyfikatorem projektu wypisanym przez demonstrację.

## Model domenowy

Model TaskFlow zawiera trzy encje:

- `Project` — projekt grupujący zadania.
- `TaskItem` — zadanie należące do projektu.
- `Comment` — komentarz dotyczący zadania.

Projekt może mieć zero lub wiele zadań, a zadanie zero lub wiele komentarzy. Powiązania opisują klucze obce `TaskItem.ProjectId` i `Comment.TaskItemId`.

Usunięcie projektu usuwa jego zadania i ich komentarze. Usunięcie zadania usuwa jego komentarze.

Typ wyliczeniowy `TaskItemStatus` zawiera wartości `Todo`, `InProgress` i `Done`.

Klasa DTO `CreateTaskRequest` opisuje dane potrzebne do utworzenia zadania: `Title`, `Description`, `DueDate` i `ProjectId`. Endpoint przyjmujący te dane nie jest jeszcze zaimplementowany.

Opisy projektu i zadania (`string?`) oraz termin wykonania zadania (`DateTime?`) mogą mieć wartość `null`.

## Autorzy

- Wiktor Lorenz

## Organizacja pracy

Zmiany są przygotowywane na gałęziach roboczych i zapisywane w logicznych commitach.

## Status

Aplikacja działa lokalnie. Sprawdzono odpowiedź HTTP 200 i tekst „API działa.”. Model TaskFlow jest połączony z bazą SQLite przez EF Core. Sprawdzono demonstrację CRUD oraz odczyt danych po ponownym uruchomieniu programu.

## Znane ograniczenia

- Jeden endpoint testowy.
- Brak endpointów CRUD i walidacji reguł domenowych.
- Lokalne uruchomienie przez HTTP.
