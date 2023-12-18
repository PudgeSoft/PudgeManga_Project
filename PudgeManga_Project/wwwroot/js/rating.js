var starClicked = false;

$(function () {

    $('.star').click(function () {

        $(this).children('.selected').addClass('is-animated pulse');

        var target = this;

        setTimeout(function () {
            $(target).children('.selected').removeClass('is-animated pulse');
        }, 1000);
        starClicked = true;
    })

    $('.half, .full').click(function () {
        if ($(this).hasClass('half')) {
            setHalfStarState(this);
        } else {
            setFullStarState(this);
        }
        var value = $(this).data('value');
        $('.score-rating').text(value); // Змінено тут
        $(this).closest('.rating').data('vote', value);

        // Заміна крапки на кому
        document.getElementById("valueSubmit").value = value;

        // AJAX-запит
        $.ajax({
            url: '@Url.Action("RateManga", "Manga")', // Замініть "ControllerName" на ім'я вашого контролера
            type: 'POST',
            data: {
                userId: $('input[name="userId"]').val(),
                mangaId: $('input[name="mangaId"]').val(),
                value: $('input[name="value"]').val()
            },
            success: function (response) {
                // Обробіть відповідь тут
            }
        });
    })


    $('.half').hover(function () {
        if (starClicked == false) {
            setHalfStarState(this)
        }
    })

    $('.full').hover(function () {
        if (starClicked == false) {
            setFullStarState(this)
        }
    })
})

function updateStarState(target) {
    $(target).parent().prevAll().addClass('animate');
    $(target).parent().prevAll().children().addClass('star-colour');

    $(target).parent().nextAll().removeClass('animate');
    $(target).parent().nextAll().children().removeClass('star-colour');
}

function setHalfStarState(target) {
    $(target).addClass('star-colour');
    $(target).siblings('.full').removeClass('star-colour');
    updateStarState(target)
}

function setFullStarState(target) {
    $(target).addClass('star-colour');
    $(target).parent().addClass('animate');
    $(target).siblings('.half').addClass('star-colour');

    updateStarState(target)
}