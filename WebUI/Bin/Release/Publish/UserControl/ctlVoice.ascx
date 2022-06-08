<%@ control language="C#" autoeventwireup="true" inherits="UserControl_ctlVoice, App_Web_tellkycl" %>
<br>
<input type="button" id='btnTalk' class="btn btn-gradient-danger waves-effect m-l-5" value="Sử dụng giọng nói"/>
<br>
<br>
<%--<br>
<span id='message'></span>
<br>--%>
<%--<br>--%>
<script>
    var message = document.querySelector('#message');
    var SpeechRecognition = SpeechRecognition || webkitSpeechRecognition;
    var SpeechGrammarList = SpeechGrammarList || webkitSpeechGrammarList;
    var grammar = '#JSGF V1.0;'

    var recognition = new SpeechRecognition();
    var speechRecognitionList = new SpeechGrammarList();
    speechRecognitionList.addFromString(grammar, 1);
    recognition.grammars = speechRecognitionList;
    recognition.lang = 'vi-VN';
    recognition.interimResults = false;

    recognition.onresult = function (event) {
        var lastResult = event.results.length - 1;
        var content = event.results[lastResult][0].transcript;
        //message.textContent = 'Voice Input: ' + content + '.';
        var old = CKEDITOR.instances['txtDes'].getData();
        CKEDITOR.instances['txtDes'].setData(old  +content);
        $("input#btnTalk").prop('disabled', false);
        $("#btnTalk").prop("value", "Sử dụng giọng nói");
    };

    recognition.onspeechend = function () {
        recognition.stop();
    };
    recognition.onerror = function (event) {
        message.textContent = 'Error occurred in recognition: ' + event.error;
    }
    document.querySelector('#btnTalk').addEventListener('click', function () {
        recognition.start();
        $("input#btnTalk").prop('disabled', true);
        $("#btnTalk").prop("value", "Bắt đầu sử dụng giọng nói");
    });
</script>
