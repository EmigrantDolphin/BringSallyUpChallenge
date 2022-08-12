<script>
    import { submitAttempt } from "../api/actions";
    let submitDisabled = false;
    const maxSeconds = 4*60;

    const handleSubmit = (e) => {
        submitDisabled = true;
        const payload = {
            seconds: e.target.elements.seconds.value,
            comment: e.target.elements.comment.value
        };
        submitAttempt(payload)
            .then(() => submitDisabled = false);
    }
</script>

<h4>Submit your attempt.</h4>
<p class='small'>(If you already had an attempt today - it will be overwritten)</p>
<form on:submit|preventDefault={handleSubmit}>
    <div class='inline'>
        <span>Seconds:</span>
        <input class='secondsInput' type='number' min=0 max={maxSeconds} name='seconds'>
        <span>Comment:</span>
        <input type='text' maxlength=2000 name='comment'>
        <button type='submit' disabled={submitDisabled}>Submit</button>
    </div>
</form>

<style>
    h4 {
        margin-bottom: -15px;
    }
    .secondsInput {
        width: 60px
    }
    .inline {
        display: inline-block;
    }
    .small {
        font-size: small;
        margin-bottom: 20px;
    }
</style>