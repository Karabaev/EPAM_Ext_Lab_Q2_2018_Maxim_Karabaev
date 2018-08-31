var contentPages = []; // контейнер со страницами
var maxCountContentPages = 20; // макс. блоков с информацией на странице
var currentPageIndex = 0; // индекс выведенной на экран страницы
var hiddenClass = "hidden"; // CSS класс для сокрытия страниц
var currentSeconds = 0; // значение счетчика
var startTimeCounter = 5; // стартовое значение счетчика
var interval = null; // таймер
var timerBtn = null; // кнопка для управления таймером

// страница загрузилась.
$(document).ready(function ()
{
    var prevBtn = $("#PrevPageBtn");
    timerBtn = $("#StopContinueTimerBtn");

    prevBtn.off();
    timerBtn.off();

    prevBtn.bind('click', PrevPageBtnClick);
    timerBtn.bind('click', StopTimer);

    for (var i = 0; i < maxCountContentPages; i++) // загрузить все блоки с id ContentN
    {
        var page = $("#Content" + i);
        if (page.text() == "")
            continue;
        contentPages.push(page);
    }

    ChangePage(0); // вывести 1 страницу
    currentSeconds = startTimeCounter; // счетчик времени в начальное положение
    StartTimer(); // запустить таймер
});


// клик по кнопке перехода на предыдущую страницу.
function PrevPageBtnClick()
{
    ChangePage(currentPageIndex - 1); // показать предыдущую страницу
}

// остановиить таймер.
function StopTimer()
{
    clearInterval(interval);
    timerBtn.off();
    timerBtn.bind('click', StartTimer);
    timerBtn.text("Start timer.");
}

// запустить таймер.
function StartTimer()
{
    Timer();
    timerBtn.off();
    timerBtn.bind('click', StopTimer);
    timerBtn.text("Stop timer.");
}

// вывести страницу по ее индексу.
function DisplayContentPage(index)
{
    for (var i = 0; i < contentPages.length; i++)
        contentPages[i].addClass(hiddenClass); // скрыть все страницы

    contentPages[index].removeClass(hiddenClass); // показать страницу с переданным индексом
}

// переключить страницу на страницу с указанным индексом.
function ChangePage(actualPageIndex)
{
    if (actualPageIndex >= contentPages.length)
    {
        if (confirm("Repeat again?")) // повторить?
            actualPageIndex = 0;
        else
            window.close();
    } 
    else
        if (actualPageIndex < 0)
            actualPageIndex = 0;
    DisplayContentPage(actualPageIndex); // вывести страницу
    currentPageIndex = actualPageIndex; // обновляем индекс показанной страницы
    $("#CurrentPageDiv").text("Current page: " + (currentPageIndex + 1));
    ChangeTimer(startTimeCounter); // сбросить счетчик таймера
}

// таймер.
function Timer()
{
    interval = setInterval(UpdateTimer, 1000);
}

// тик таймера.
function UpdateTimer()
{
    ChangeTimer(currentSeconds - 1); // уменьшить счетчик таймера 1

    if (currentSeconds <= 0)
    {
        ChangePage(currentPageIndex + 1);
    }
}

// изменение счетчика таймера.
function ChangeTimer(value)
{
    currentSeconds = value;
    $("#TimerDiv").text(currentSeconds + " sec.");
}

