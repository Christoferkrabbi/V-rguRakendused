import { type Person } from "@/models/person";
import { ref } from "vue";
import { defineStore } from "pinia";
import useApi, { useApiRawRequest } from "@/models/api";

export const usePeopleStore = defineStore('peopleStore', () => {
  const apiGetPeople = useApi<Person[]>('people');
  const people = ref<Person[]>([]);
  let allPeople: Person[] = [];

  const loadPeople = async () => {
    await apiGetPeople.request();

    if (apiGetPeople.response.value) {
      return apiGetPeople.response.value;
    }
    return [];
  };

  const load = async () => {
    allPeople= await loadPeople();
    people.value = allPeople;
  };
  const getPeopleById = (id: Number) => {
    return allPeople.find((person) => person.id === id);
  };


  const addPerson = async (person: Person) => {
    const apiAddPerson = useApi<Person>('people', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(person),
    }); 
    
    await apiAddPerson.request();
      if (apiAddPerson.response.value) {
      load();      
    }
  };
  const updatePerson = async (person: Person) => {
    const apiUpdatePerson = useApi<Person>('people/' + person.id, {
      method: 'PUT',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(person),
    });

    await apiUpdatePerson.request();
      if (apiUpdatePerson.response.value) {
      load();
    }    
  };


  const deletePerson = async (person: Person) => {
    const deletePersonRequest = useApiRawRequest(`events/${person.id}`, {
      method: 'DELETE',
    });

    const res = await deletePersonRequest();

    if (res.status === 204) {
      let id = people.value.indexOf(person);

      if (id !== -1) {
        people.value.splice(id, 1);
      }

      id = people.value.indexOf(person);

      if (id !== -1) {
        people.value.splice(id, 1);
      }
    }
  };

  return { people, load, getPeopleById, addPerson, updatePerson, deletePerson };
});