$(function(){
	$('.nav button.menu').click(function(){
		$('.nav').toggleClass('expand');
	});

	$('.nav li,.assort li').click(function(){
		window.location = $(this).find('a').attr('href');
	});

	$(window).scroll(function(){
		if($(window).scrollTop() > 500 && window.innerWidth > 960){
			$("#gotop").fadeIn(500);
		}else{
			$("#gotop").fadeOut(500);
		}
	});

	$('#gotop').click(function(){
		$('body,html').animate({scrollTop:0},1000);
	});

	/**
	 * Hack content static URL
	 */
	var $hackUrl = $('.J-hack-url');
	if ($hackUrl.length) {
		var baseURL = $hackUrl.data('url');
		$('.J-hack-url img').each(function() {
			var src = $(this).attr('src');
			var newSrc = src.replace(/^\.\.\//, baseURL);
			$(this).attr('src', newSrc);
		});
	}
});