
<script>
    import moment from 'moment';
    import { onMount } from 'svelte';
    import {AttemptStore} from '../stores';
    import { getAttempts } from '../api/actions';

    export let userId;

    onMount(() => {
        getAttempts(userId);
    })
</script>

<table>
    <tr>
        <th>User</th>
        <th>Seconds</th>
        <th>Improvement</th>
        <th>Comment</th>
        <th>Date of attempt</th>
    </tr>
    {#each $AttemptStore as data , i}
        <tr class={i % 2 === 0 ? 'dim' : ''}>
            <td>{data.username}</td>
            <td class='seconds'>{data.seconds}</td>
            <td
                class={data.improvement > 0 ? 'green improvement' : data.improvement < 0 ? 'red improvement' : 'black improvement'}
            >
                    {data.improvement === null ? '' : data.improvement}
            </td>
            <td>{data.comment}</td>
            <td>{moment(data.dateOfExecution).format('yyyy/MM/DD HH:mm:ss')} ({moment(data.dateOfExecution).fromNow()})</td>
        </tr>
    {/each}
</table>

<style>
    table {
        margin-left: auto;
        margin-right: auto;
    }
    .red {
        color: red;
    }
    .green {
        color: green;
    }
    .black {
        color: black;
    }
    .dim {
        background-color: #f0eded;
    }
    .seconds {
        width: 4%;
    }
    .improvement {
        width: 4%
    }
    td {
        width: 10%;
    }
</style>