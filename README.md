# PZL-Lorenz-Wiktor

Testowa aplikacja ASP.NET Core przygotowana w ramach laboratorium z Programowania zaawansowanego.

## Cel projektu

- Sprawdzenie środowiska ASP.NET Core i nauka pracy z Git.

## Technologie

- C#
- .NET 10
- ASP.NET Core
- Git

## Wymagania środowiskowe

- .NET SDK: .NET SDK 10.0.x — projekt uruchomiono z wersją 10.0.402.
- IDE: Visual Studio Code.

Plik `global.json` wybiera najnowsze zainstalowane stabilne SDK z linii 10.0.x.

## Uruchomienie

```powershell
dotnet run --project src/PZL.TestApi --launch-profile http
```

Otwórz w przeglądarce http://localhost:5114/.

Endpoint GET / zwraca tekst „API działa.”.

## Model domenowy

Model TaskFlow zawiera trzy encje:

- `Project` — projekt grupujący zadania.
- `TaskItem` — zadanie należące do projektu.
- `Comment` — komentarz dotyczący zadania.

Projekt może mieć zero lub wiele zadań, a zadanie zero lub wiele komentarzy. Powiązania opisują klucze obce `TaskItem.ProjectId` i `Comment.TaskItemId`.

Typ wyliczeniowy `TaskItemStatus` zawiera wartości `Todo`, `InProgress` i `Done`.

Klasa DTO `CreateTaskRequest` opisuje dane potrzebne do utworzenia zadania: `Title`, `Description`, `DueDate` i `ProjectId`. Endpoint przyjmujący te dane nie jest jeszcze zaimplementowany.

Opisy projektu i zadania (`string?`) oraz termin wykonania zadania (`DateTime?`) mogą mieć wartość `null`.

## Autorzy

- Wiktor Lorenz

## Organizacja pracy

Zmiany są przygotowywane na gałęziach roboczych i zapisywane w logicznych commitach.

## Status

Aplikacja działa lokalnie. Sprawdzono odpowiedź HTTP 200 i tekst „API działa.”. Przygotowano model domenowy TaskFlow.

## Znane ograniczenia

- Projekt jest na etapie szkieletu aplikacji i modelu domenowego.
- Jeden endpoint testowy.
- Brak bazy danych, operacji CRUD i walidacji reguł domenowych.
- Lokalne uruchomienie przez HTTP.
