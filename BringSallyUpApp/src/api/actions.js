import { get } from 'svelte/store';
import { pageConstants } from '../pages/PageConstants';
import { UserIdStore, CurrentPageStore, AttemptStore } from '../stores';
import { requestMethods, sendRequest } from "./sendRequest"

export const login = (loginPayload) => {
    return sendRequest('/login', requestMethods.POST, loginPayload)
    .then((response => {
        if (response.status === 200){
            response.json().then((data) => {
                UserIdStore.update(() => {
                    return data.userId;
                });
                CurrentPageStore.update(() => {
                    return pageConstants.mainFeedPage;
                });
            });
        }
        else {
            console.log('pop some error');
        }
    }));
}

export const submitAttempt = (payload) => {
    const userId = get(UserIdStore);
    payload = {
        userId: userId,
        ...payload
    };
    console.log(payload);
    return sendRequest('/attempt', requestMethods.POST, payload)
    .then((response => {
        if (response.status === 200){
            const usernames = get(AttemptStore).map(x => x.username);
            const distinctUsernames = usernames.filter((value, index, self) => {
                return self.indexOf(value) == index;
            });
            
            if (distinctUsernames.length == 1) {
                getAttempts(userId);
            }
            else{
                getAttempts();
            }
        }
        else {
            console.log('pop some error');
        }
    }));
}

export const getAttempts = (userId = null) => {
    const url = userId ? `/attempts?userId=${userId}` : '/attempts';
    return sendRequest(url, requestMethods.GET)
    .then((response => {
        if (response.status === 200){
            response.json().then((data) => {
                AttemptStore.update(() => {
                    return data;
                })
            })
        }
        else {
            console.log('pop some error');
        }
    }));
}