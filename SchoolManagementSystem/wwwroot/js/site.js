/**
 * CREATOR: SAMUEL WENDOLIN
 * DATE: MAR 1,2019
 * */

let btt = $('.back-to-top');
$(window).on('scroll', function () {
    let self = $(this),
        height = self.height(),
        top = self.scrollTop();

    if (top > height) {
        if (!btt.is(':visible')) {
            btt.fadeIn();
        }
    } else {
        btt.fadeOut();
    }
});

btt.on('click', function (e) {
    e.preventDefault();
    $('html,body').animate({
        scrollTop: 0
    }, 2000);
});