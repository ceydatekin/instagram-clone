//https://d13yacurqjgara.cloudfront.net/users/1387536/screenshots/3115905/dribbble_20161126.png
//https://s-media-cache-ak0.pinimg.com/564x/5f/cc/e4/5fcce4b2de8e80e1364146be06b1c015.jpg
//http://abload.de/img/gta_sa2015-09-1323-33pgunm.png
//http://inspiring.online/
// https://d13yacurqjgara.cloudfront.net/users/1014040/screenshots/3134277/softwood_sky_za_animaciju_2.gif
var inf_scroll = false;

// Database for the page - Will be populated by the API Call
var db = { img: {} };

// Assign pictures to the initial images
function init_images() {
    var total_images = $('.image').length;
    for (var i = 0; i < total_images; i++) {
        $('.image').eq(i).addClass('image' + i);
        $('.image' + i).css('background', 'url("' + db.img[i].src + '") center no-repeat')
    }
}

// Add more images and assigning the background images
function add_images() {
    var total_images = $('.image').length; // Getting total number of images on the page.

    for (var i = 1; i < 13; i++) {
        var this_num = total_images + i;

        // Check for end of images
        if (this_num > Object.keys(db.img).length) { $('.more-images').addClass('inactive'); inf_scroll = false; return false; };

        $('.content').append('<div class="rela-inline image image' + this_num + '"></div>');
        $('.image' + this_num).css('background', 'url("' + db.img[this_num - 1].src + '") center no-repeat')
    };

    if ((total_images + 7) > Object.keys(db.img).length) {
        $('.more-images').addClass('inactive');
        inf_scroll = false;
    };
};

// Overlay display and hide functions
function display_overlay(image_num) {
    $('.post-image').css('background', 'url("' + db.img[image_num].src + '") center no-repeat');
    $('.post-image').attr('name', db.img[image_num].link);
    $('.desc-title').text(db.img[image_num].title);
    $('.desc-author').text('By: ' + db.img[image_num].author);
    $('.desc-desc').html(db.img[image_num].desc);
    $('.overlay').addClass('active');
    //$('.container, .nav-bar, .footer').addClass('blurred');
}
function hide_overlay() {
    $('.post-image').css('background', 'url("") center no-repeat');
    $('.post-image').attr('name', '');
    $('.desc-title').text('');
    $('.desc-author').text('');
    $('.desc-desc').text('');
    $('.overlay').removeClass('active');
    //$('.container, .nav-bar, .footer').removeClass('blurred');
}

// --- API Stuff ---
function api_call() {
    $.getJSON('https://api.dribbble.com/v1/shots?per_page=100&access_token=d7b35bf8f107cf7310e1291a3ab25fe50bb9a7ade70ee8e46170bd4d4b256087&callback=?', function (resp) {
        if (resp.data.length > 0) {
            $.each(resp.data, function (i, val) {
                db.img[i] = { src: val.images.hidpi, desc: val.description, title: val.title, author: val.user.name, author_pic: val.user.avatar_url, link: val.html_url }
            });
        }
        console.log(resp);
        init_images();
    });
}

// --- GAME TIME ---
$(document).ready(function () {
    // API Call and Image Initiallization
    api_call();

    // Displaying and hiding overlay
    $('.content').on('click', '.image', function () {
        display_overlay($('.image').index($(this)));
    });
    $('.overlay, .close').click(function () { hide_overlay() });

    // The overlay image links the the original Dribbble post page
    $('.post-image').click(function () {
        var new_window = window.open($(this).attr('name'), '_blank');
        new_window.focus();
    });

    $('.overlay-card').click(function () { return false });

    $(window).scroll(function () {
        //console.log( $(window).scrollTop() + $(window).height() - $(document).height())
        if (($(window).scrollTop() + $(window).height() - $(document).height()) > -1000 && (inf_scroll)) {
            add_images();
            console.log(inf_scroll)
        }
    });

    // Toggling the nav menu
    $('.menu-toggle').click(function () { $('.menu-background, .menu-card, .menu-content').toggleClass('active'); });
    // Toggle the nav-search
    $('.search-icon').click(function () { $('.nav-search, .sign-div').toggleClass('active') });
})@