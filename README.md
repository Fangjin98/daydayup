<p align="center">
  <a href="#">
  </a>
  <img width="64" src="/images/logo.png">
</p>

<h1 align="center">
  DayDayUp
</h1>

<p align="center">
  A Light-weight todo manager with time estimation tracking.
</p>

<p align="center">
  <img src="/images/homepage.png">
</p>

## Introduction

Time estimation is a key need for todo management. DayDayUp helps you to better estimate the completion time of todos via evidence-based scheduling (EBS).

## Background

### What is the EBS?

[EBS](https://fogbugz.com/Evidence-Based-Scheduling/#:~:text=Evidence%20Based%20Scheduling%20or%20EBS%20is%20a%20statistical,the%20probability%20that%20your%20project%20will%20be%20completed) is a statistical algorithm that produces ship date probability distributions. It gathers evidence, mostly from historical timesheet data and provides accurate schedules. 

EBS produces a probability distribution curve, so that you know for any given date, the probability that your project will be completed.

### How the EBS works?

In DayDayUp, each todo has three attributes:

1. _real duration_ : record by DayDayUp, after users finish a todo.
2. _estimated duration_: **set by users** when (after) a todo is created. It means that, this todo is supposed to take _estimated duration_ mins to finish.
3. _predicted duration_: calculate by DayDayUp as the results of the EBS. It is a set of values, representing the bias of _estimated duration_ under different probabilities.

After one todo is created, users can set the _estimated duration_.

For each unfinished todo, DayDayUp adopts Monte Carlo Method to calculate _predicted durations_, based on the bias of _real durations_ and _estimated durations_ of finished todos.

## Install

### Building from source

Make sure you have installed:

- Git
- [Visual Studio 2022](https://visualstudio.microsoft.com/zh-hans/vs/), community edition works.

Clone the repository with `git clone https://github.com/Fangjin98/daydayup-winui3`

Open `src/daydayup-winui3.sln` and hit F5 to compile and run.

## Screenshots

<p align="center">
  <img src="/images/screenshot_1.png">
</p>

<p align="center">
  <img src="/images/screenshot_2.png">
</p>

<p align="center">
  <img src="/images/screenshot_3.png">
</p>

## Roadmap

Status |          Features     |  Memo  |
--        |  ------------------------ |   -----  |
✅| _Create Todos_ | Set estimated duration of todos|
✅| _Start & Pause Todos_ | Switch status of todos|
✅| _Per-todo Informations_ | Estimated duration, Prediction durations and Current duration|
✅| _Multi-language Support_ | |
🔁|_Data Export_| |
🔲|_Multi-device Synchronization_| |
🔲|_Category_|Todos can be assigned to different categories|
🔲|_Dashboard_|Statistics summary|
🔲| _CLI Support_ | |

✅ Supported | 🔁 In progress | 🔲 Not started

## Acknowledgments

- [.NET Community Toolkit](https://github.com/CommunityToolkit/MVVM-Samples)
- [WinUIEx](https://github.com/dotMorten/WinUIEx)
- [SettingsUI](https://github.com/WinUICommunity/SettingsUI)
