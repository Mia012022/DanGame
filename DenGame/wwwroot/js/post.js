// 輸入圖片路徑 顯示照片
//class MyUploadAdapter {
//	constructor(loader) {
//		// The file loader instance to use during the upload.
//		this.loader = loader;
//	}

//	// Starts the upload process.
//	upload() {
//		var reader = new FileReader();

//		return new Promise((resolve, reject) => {
//			reader.addEventListener('load', () => {
//				resolve({ default: reader.result });
//			});

//			reader.addEventListener('error', err => {
//				reject(err);
//			});

//			reader.addEventListener('abort', () => {
//				reject();
//			});

//			this.loader.file.then(file => {
//				reader.readAsDataURL(file);
//			});
//		})
//	}

//	// Aborts the upload process.
//	abort() {
//		if (this.xhr) {
//			this.xhr.abort();
//		}
//	}

//	// Initializes the XMLHttpRequest object using the URL passed to the constructor.
//	_initRequest() {
//		const xhr = (this.xhr = new XMLHttpRequest());

//		// Note that your request may look different. It is up to you and your editor
//		// integration to choose the right communication channel. This example uses
//		// a POST request with JSON as a data structure but your configuration
//		// could be different.
//		xhr.open("POST", "~/img", true);
//		xhr.responseType = "json";
//	}

//	// Initializes XMLHttpRequest listeners.
//	_initListeners(resolve, reject, file) {
//		const xhr = this.xhr;
//		const loader = this.loader;
//		const genericErrorText = `無法上傳檔案: ${file.name}.`;

//		xhr.addEventListener("error", () => reject(genericErrorText));
//		xhr.addEventListener("abort", () => reject());
//		xhr.addEventListener("load", () => {
//			const response = xhr.response;

//			console.log('response', response);

//			// This example assumes the XHR server's "response" object will come with
//			// an "error" which has its own "message" that can be passed to reject()
//			// in the upload promise.
//			//
//			// Your integration may handle upload errors in a different way so make sure
//			// it is done properly. The reject() function must be called when the upload fails.
//			if (!response || response.error) {
//				return reject(response && response.error ? response.error.message : genericErrorText);
//			}

//			// If the upload is successful, resolve the upload promise with an object containing
//			// at least the "default" URL, pointing to the image on the server.
//			// This URL will be used to display the image in the content. Learn more in the
//			// UploadAdapter#upload documentation.
//			resolve({
//				default: response.url,
//			});
//		});

//		// Upload progress when it is supported. The file loader has the #uploadTotal and #uploaded
//		// properties which are used e.g. to display the upload progress bar in the editor
//		// user interface.
//		if (xhr.upload) {
//			xhr.upload.addEventListener("progress", evt => {
//				if (evt.lengthComputable) {
//					loader.uploadTotal = evt.total;
//					loader.uploaded = evt.loaded;
//				}
//			});
//		}
//	}

//	// Prepares the data and sends the request.
//	_sendRequest(file) {
//		// Prepare the form data.
//		const data = new FormData();

//		data.append("Files", file);

//		console.log('file:', file);

//		// Important note: This is the right place to implement security mechanisms
//		// like authentication and CSRF protection. For instance, you can use
//		// XMLHttpRequest.setRequestHeader() to set the request headers containing
//		// the CSRF token generated earlier by your application.

//		// Send the request.
//		this.xhr.send(data);
//	}
//}

//// ...

//function MyCustomUploadAdapterPlugin(editor) {
//	editor.plugins.get("FileRepository").createUploadAdapter = loader => {
//		// Configure the URL to the upload script in your back-end here!
//		return new MyUploadAdapter(loader);
//	};
//}




var myEditor;
ClassicEditor
	.create(document.querySelector('#editor'), {
	
	})
	.then(editor => {
		console.log(editor);
		myEditor = editor;
	})
	.catch(error => {
		console.error(error);
	});
$(document).ready(function () {
	$('#imageInput').on('change', function (e) {
		var file = e.target.files[0];
		var reader = new FileReader();

		reader.onload = function (e) {
			var imageData = e.target.result;
			$('#closeLabel').after('<div id="imageDisplayArea" style="width:100%;"></div>');
			$('#imageDisplayArea').html('<img src="' + imageData + '" alt="Selected Image" style="width:100%;height:500px;object-fit:cover;">');
			$('#closeLabel').css('display', 'none');
			$('#deleteImage').show();
			$('#editImage').show();
		};

		reader.readAsDataURL(file);
	});
	//移除照片
	$('#deleteImage').on('click', function () {
		event.preventDefault();
		$('#imageDisplayArea').remove();
		$('#imageInput').val('');
		$('#deleteImage').hide();
		$('#closeLabel').show();
	});
	// 監聽字數
	$('#textInput').on('input', function () {
		var currentLength = $(this).val().length;
		$('#charCount').text(currentLength + '/200');
	});

	//預覽畫面
	$('#previewBtn').on('click', function () {
		event.preventDefault();

		var title = $('#textInput').val();
		var content = $('.editor').val();
		var image = $('#imageInput')[0].files[0];
		$('#previewTitle').text(title);
		$('#previewContent').html(myEditor.getData());

		if (image) {
			var reader = new FileReader();
			reader.onload = function (e) {
				$("#previewImage").attr("src", e.target.result).show();
			}
			reader.readAsDataURL(image);
		} else {
			$("#previewImage").hide();
		}
		$('#preview').show();
	});
	$('.close').on('click', function () {
		$('#preview').hide();
	});

	
});