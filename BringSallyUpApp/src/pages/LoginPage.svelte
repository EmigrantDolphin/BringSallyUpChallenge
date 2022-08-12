
<script>
    import { login } from '../api/actions';
    const minUsernamePasswordLength = 4;
    let disableSubmit = true;
    let username = '';
    let plainPassword = '';

    const handleUsernamePassword = () => {
        if ((username.length < minUsernamePasswordLength) || (plainPassword.length < minUsernamePasswordLength)) {
            disableSubmit = true;
        }
        else{
            disableSubmit = false;
        }
    };

    const handleSubmit = () => {
        disableSubmit = true;

        const loginPayload = {
            username: username,
            plainPassword: plainPassword,
        }

        login(loginPayload)
            .then(() => disableSubmit = false);
    };
</script>

<form on:submit|preventDefault={handleSubmit}>
    <h4>Identify yourself :)</h4>
    <table class='login-wrapper'>
        <tr>
            <td>Username:</td>
            <td><input type='text' bind:value={username} on:input={handleUsernamePassword} minlength=4></td>
        </tr>
        <tr>
            <td>Plain password:</td>
            <td><input type='text' bind:value={plainPassword} on:input={handleUsernamePassword} minlength=4></td>
        </tr>
    </table>
    <button type='submit' disabled={disableSubmit}>Login</button>
<br /> <p style='font-size: small'>(please don't use real password. There is zero security.)</p>
</form>

<style>
    .login-wrapper {
        margin-left: auto;
        margin-right: auto;
    }
    td {
        vertical-align: top;
    }
</style>