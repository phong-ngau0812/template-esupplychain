$('.alert-close').on("click", showAlert);
function showAlert() {
    $('.first-alert').addClass('close');
}

$('.son1').on("click", activeOn1);
function activeOn1() {
    if ($('.son1').hasClass('active-on')) {
    }
    else {
        $('.son1').addClass('active-on');
        $('.son2').removeClass('active-on');
        $('.son3').removeClass('active-on');
        $('.content-product').addClass('active-show');
        $('.content-trademark').removeClass('active-show');
        $('.content-history').removeClass('active-show');
    }
}

$('.son2').on("click", activeOn2);
function activeOn2() {
    if ($('.son2').hasClass('active-on')) {
    }
    else {
        $('.son2').addClass('active-on');
        $('.son1').removeClass('active-on');
        $('.son3').removeClass('active-on');
        $('.content-history').addClass('active-show');
        $('.content-product').removeClass('active-show');
        $('.content-trademark').removeClass('active-show');
    }
}

$('.son3').on("click", activeOn3);
function activeOn3() {
    if ($('.son3').hasClass('active-on')) {
    }
    else {
        $('.son3').addClass('active-on');
        $('.son2').removeClass('active-on');
        $('.son1').removeClass('active-on');
        $('.content-trademark').addClass('active-show');
        $('.content-product').removeClass('active-show');
        $('.content-history').removeClass('active-show');
    }
}




$('.show1').on("click", activeShow1);
function activeShow1() {
    if ($('.line-hover1').hasClass('active-show')) {
    }
    else {
        $('.line-hover1').addClass('active-show');
        $('.line-hover2').removeClass('active-show');
        $('.menu-son-li1').addClass('menu-color');
        $('.menu-son-li2').removeClass('menu-color');
        
    }
}

$('.show2').on("click", activeShow2);
function activeShow2() {
    if ($('.line-hover2').hasClass('active-show')) {
    }
    else {
        $('.line-hover2').addClass('active-show');
        $('.line-hover1').removeClass('active-show');
        $('.menu-son-li2').addClass('menu-color');
        $('.menu-son-li1').removeClass('menu-color');
    }
}



