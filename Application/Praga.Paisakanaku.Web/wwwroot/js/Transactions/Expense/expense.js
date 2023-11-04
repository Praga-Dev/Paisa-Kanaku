const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'INR'
});

$(document).ready(function () {  
    getExpenseData();
});

function getExpenseData() {
    loadSpinner();
    $.ajax({
        url: `/expense/list/data`,
        method: 'GET',
        success: function (response) {
            if (response && response.data && response.isSuccess) {
                // get data
                let expenseInfoList = response.data;
                let calEvents = [];

                //for (var exp in expenseInfoList) { 
                //}

                expenseInfoList.forEach((exp) => {
                    let data = {
                        title: '- ' + formatter.format(exp.amount),
                        start: exp.date,
                        end: null
                    }

                    calEvents.push(data);
                });
                console.log(calEvents);
                InitializeCalendar(calEvents);
            }
            else {
                // TODO Alert
            }
        },
        error: function () {
            // TODO Alert
        },
        complete: function () {
            hideSpinner();
        }
    })
}

function InitializeCalendar(calEvents) {
    if (calEvents) {

        var calendarEl = $('#calendar')[0];

        var calendar = new FullCalendar.Calendar(calendarEl, {
            height: 580,
            initialDate: '2023-10-10',
            //editable: true,
            selectable: true,
            //businessHours: true,
            dayMaxEvents: true, // allow "more" link when too many events
            events: calEvents,
            displayEventTime: false,
            eventDisplay: 'block',
            displayEventEnd: false,
            eventDidMount: function (event) {
                if (event) {
                    var title = $(event.el).find('.fc-event-title')
                    title.html(title.text());
                }
            }
        });

        calendar.render();
    }
}