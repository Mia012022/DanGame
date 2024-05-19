$('.reply').on('click', function () {
	
	$(this).closest('.userComment').append('<div class="input-group mb-4 replyInput"><input type = "text" class = "form-control" placeholder = "快來發表你的評論吧" ><button class="btn btn-outline-secondary"><i class="bi bi-emoji-smile  "></i></button></i ><button class="btn btn-outline-secondary">發送</button></div > ');
	$(this).attr('disabled', true);
	
})