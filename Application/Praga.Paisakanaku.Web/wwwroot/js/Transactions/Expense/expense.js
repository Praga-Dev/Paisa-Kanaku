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
                let expenseInfoList = response.data;
                let calEvents = [];
                

                expenseInfoList.forEach((exp) => {
                    let color = exp.amount > 1000 ? 'darkred' : 'yellow'
                    let textColor = exp.amount > 1000 ? 'white' : 'black'

                    let data = {
                        title: '- ' + formatter.format(exp.amount),
                        start: exp.date,
                        end: exp.date,
                        color: color,
                        textColor: textColor
                    }

                    calEvents.push(data);
                });
                
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
            initialDate: new Date(),
            //editable: true,
            selectable: true,
            //businessHours: true,
            dayMaxEvents: true, // allow "more" link when too many events
            events: calEvents,
            displayEventTime: false,
            eventDisplay: 'block',
            displayEventEnd: false,
            //eventDidMount: function (event) {
            //    if (event) {
            //        var title = $(event.el).find('.fc-event-title')
            //        title.html(title.text());
            //    }
            //}
        });

        calendar.render();
    }
}