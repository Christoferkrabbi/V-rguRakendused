<template>
  <div class="about">
    <h1>Peoplelist</h1>
    <div class="peoplelist">
        <DataTable :value="people" v-if="people.length > 0 ">
            <Column field="id" header="Isiku id" style="color: black; " />
            <Column field="name" header="nimi" style="color: black; " />
            <Column field="age" header="Vanus" style="color: black;" />
            <Column header="tegevused">
                <template #body=" SlotProps">
                    <button @click="Delete(slotProps.data.id)">Kustuta</button>
                </template>
            </Column>
        </DataTable>
        <div v-else>Isikud puuduvad</div>

        <!--<div class="add-people-form">
            <h2>Lisa Inimene</h2>
            <form @submit.prevent="Create">
                <div>
                    <label for="name">Nimi:</label>
                    <input v-model="newPerson.name" type="text" id="name" required />
                </div>
                <div>
                    <label for="age">Vanus:</label>
                    <input v-model="newPerson.age" type="number" id="age" required />
                </div>
                <button type="submit">Lisa inimene</button>
            </form>
        </div>-->

        <!--<button @click="Create">loo</button>-->
    </div>
  </div>
</template>


<script setup lang="ts">
import { type Person } from '@/models/person';
import { usePeopleStore } from "@/stores/peopleStore";
import { storeToRefs } from "pinia";
import { defineProps, onMounted, watch, ref  } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();

watch(route, (to, from) => {
  if (to.path !== from.path || to.query !== from.query) {
    peopleStore.load();
  }
}, { deep: true });

defineProps<{ title: String }>();
const peopleStore = usePeopleStore();
const { people } = storeToRefs(peopleStore);

onMounted(async () => {
  peopleStore.load();
});
</script>
<style>

</style>