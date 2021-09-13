$('body').on('click', '#register-submit', function () {

    var name = $('#name').val();
    var surname = $('#surname').val();
    var userName = $('#firstusername').val();
    var telno = $('#firstpassword').val();
    var mail = $('#mail').val();
    var password = $('#telno').val();

    var formdata = new FormData();

    formdata.append('name', name);
    formdata.append('surname', surname);
    formdata.append('userName', userName);
    formdata.append('telno', telno);
    formdata.append('mail', mail);
    formdata.append('password', password);
    formdata.append('profilePhotoFile', $('#profilePhotoFile')[0].files[0]);

    $.ajax({
        url: '/KullaniciKaydet',
        method: 'post',
        data: formdata,
        processData: false,
        contentType: false,
        success: function (resp) {
            console.log(resp)
            var data = JSON.parse(resp)
            if (data.success == true) {
                $('#ogrenciEkle').modal('hide')

                console.log(resp.message);
            }
            else if (data.success == false)
                console.log("ders eklenirken hata olust")
        },
        error: function (err) {
            console.log(err)
        }
    });
});



$('body').on('keyup', '#password', function (event) {

    if (event.keyCode == 13) {
        event.preventDefault();
        document.getElementById("login-submit").click();
    }
});
$('body').on('keyup', '#username', function (event) {

    if (event.keyCode == 13) {
        event.preventDefault();
        document.getElementById("login-submit").click();
    }
});


$('body').on('click', '#login-submit', function () {

    var kullaniciAdi = $('#username').val();
    var sifre = $('#password').val();

    var formdata = new FormData();

    formdata.append('kullaniciAdi', kullaniciAdi);
    formdata.append('sifre', sifre);


    $.ajax({
        url: '/girisYapPost',
        method: 'post',
        data: formdata,
        processData: false,
        contentType: false,
        success: function (resp) {

            if (resp == true) {
                window.location.href = "/"
                console.log("buradayız2");
            }
            else {
                alert("hatalı Giriş")
                window.location.reload(2);
                console.log("buradayız3");


            }
        },
        error: function (err) {
            console.log(err)
        }
    });
});



$(function () {
    var log = document.getElementById('login-form');
    var rag = document.getElementById('register-form');

    $('#login-form-link').click(function (e) {
        rag.style.visibility = 'hidden';
        log.style.visibility = 'visible';
        $("#login-form").delay(100).fadeIn(100);
        $("#register-form").fadeOut(100);
        $('#register-form-link').removeClass('active');
        $(this).addClass('active');
        e.preventDefault();
    });
    $('#register-form-link').click(function (e) {

        rag.style.visibility = 'visible';
        log.style.visibility = 'hidden';
        $("#register-form").delay(100).fadeIn(100);
        $("#login-form").fadeOut(100);
        $('#login-form-link').removeClass('active');
        $(this).addClass('active');
        e.preventDefault();
    });

});