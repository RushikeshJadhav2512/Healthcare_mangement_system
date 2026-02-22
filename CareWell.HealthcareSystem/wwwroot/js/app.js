// Modern SPA frontend (module) — communicates via JSON with the backend API
const api = {
  patients: '/api/patients',
  appointments: '/api/appointments'
};

const qs = (s, ctx = document) => ctx.querySelector(s);
const qsa = (s, ctx = document) => Array.from(ctx.querySelectorAll(s));

function showToast(message, title = 'Notice', type = 'primary'){
  const container = qs('#toast-container');
  const id = 't' + Math.random().toString(36).slice(2,9);
  const toast = document.createElement('div');
  toast.className = `toast align-items-center text-bg-${type} border-0`;
  toast.id = id;
  toast.setAttribute('role','alert');
  toast.setAttribute('aria-live','assertive');
  toast.setAttribute('aria-atomic','true');
  toast.innerHTML = `
    <div class="d-flex">
      <div class="toast-body">
        <strong>${title}:</strong> ${message}
      </div>
      <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
    </div>
  `;
  container.appendChild(toast);
  const b = new bootstrap.Toast(toast, { delay: 4000 });
  b.show();
  toast.addEventListener('hidden.bs.toast', () => toast.remove());
}

async function fetchJson(url, options){
  const res = await fetch(url, options);
  if (!res.ok){
    const text = await res.text();
    throw new Error(text || res.statusText);
  }
  return res.json();
}

// ===== Patients =====
async function loadPatients(){
  try{
    const data = await fetchJson(api.patients);
    return data;
  }catch(err){
    showToast(err.message || 'Unable to load patients', 'Error', 'danger');
    return [];
  }
}

function patientCard(p){
  const div = document.createElement('div');
  div.className = 'card fade-in';
  div.innerHTML = `
    <div class="card-body d-flex gap-3 align-items-center">
      <div class="patient-avatar">${(p.firstName[0]||'').toUpperCase()}${(p.lastName[0]||'').toUpperCase()}</div>
      <div class="flex-grow-1">
        <div class="d-flex justify-content-between">
          <div>
            <h5 class="mb-0">${escapeHtml(p.firstName)} ${escapeHtml(p.lastName)}</h5>
            <div class="muted small">${escapeHtml(p.email)} • ${escapeHtml(p.phone)}</div>
          </div>
          <div class="text-end">
            <button class="btn btn-sm btn-outline-primary me-1 schedule-btn" data-id="${p.id}">Schedule</button>
            <button class="btn btn-sm btn-ghost text-danger delete-patient" data-id="${p.id}">Delete</button>
          </div>
        </div>
      </div>
    </div>
  `;
  return div;
}

function escapeHtml(str){
  if (!str) return '';
  return String(str).replace(/[&<>"']/g, s => ({'&':'&amp;','<':'&lt;','>':'&gt;','"':'&quot;',"'":"&#39;"})[s]);
}

async function renderPatients(){
  const app = qs('#app');
  app.innerHTML = '';

  const header = document.createElement('div');
  header.className = 'd-flex justify-content-between align-items-center mb-4';
  header.innerHTML = `
    <div>
      <h2 class="h4">Patients</h2>
      <p class="muted mb-0">Register and manage clinic patients</p>
    </div>
    <div>
      <button class="btn btn-primary" id="new-patient">New Patient</button>
    </div>
  `;
  app.appendChild(header);

  const listContainer = document.createElement('div');
  listContainer.id = 'patients-list';
  app.appendChild(listContainer);

  const patients = await loadPatients();
  if (!patients || patients.length === 0){
    listContainer.innerHTML = '<div class="alert alert-info">No patients yet. Click "New Patient" to add one.</div>';
  } else {
    patients.forEach(p => listContainer.appendChild(patientCard(p)));
  }

  // New patient modal (inline)
  qs('#new-patient').addEventListener('click', () => openNewPatientModal());
  qsa('.schedule-btn', listContainer).forEach(btn => btn.addEventListener('click', (e) => {
    const id = e.target.dataset.id;
    openScheduleModal(id);
  }));
  qsa('.delete-patient', listContainer).forEach(btn => btn.addEventListener('click', async (e) => {
    const id = e.target.dataset.id;
    if (!confirm('Delete patient?')) return;
    await fetch(api.patients + '/' + id, { method: 'DELETE' });
    showToast('Patient deleted', 'Deleted', 'secondary');
    renderPatients();
  }));
}

// New patient modal using built-in prompt card
function openNewPatientModal(){
  const app = qs('#app');
  const modalCard = document.createElement('div');
  modalCard.className = 'card mb-3';
  modalCard.innerHTML = `
    <div class="card-body">
      <h5 class="card-title">Register Patient</h5>
      <form id="patient-form" class="row g-2 needs-validation" novalidate>
        <div class="col-md-6"><input required name="firstName" class="form-control" placeholder="First name" /></div>
        <div class="col-md-6"><input required name="lastName" class="form-control" placeholder="Last name" /></div>
        <div class="col-md-4"><input required name="dob" type="date" class="form-control" /></div>
        <div class="col-md-4"><input required name="email" type="email" class="form-control" placeholder="email@host" /></div>
        <div class="col-md-4"><input required name="phone" class="form-control" placeholder="Phone" /></div>
        <div class="col-12 text-end"><button class="btn btn-success mt-2">Register</button>
          <button type="button" class="btn btn-link mt-2" id="cancel-new">Cancel</button></div>
      </form>
    </div>
  `;
  app.prepend(modalCard);
  qs('#cancel-new').addEventListener('click', () => modalCard.remove());
  qs('#patient-form').addEventListener('submit', async (ev) => {
    ev.preventDefault();
    const form = ev.target;
    const data = Object.fromEntries(new FormData(form).entries());
    // convert dob
    if (data.dob) data.dateOfBirth = data.dob;
    const payload = {
      firstName: data.firstName,
      lastName: data.lastName,
      dateOfBirth: data.dateOfBirth,
      email: data.email,
      phone: data.phone
    };
    try{
      await fetchJson(api.patients, { method: 'POST', headers: {'Content-Type':'application/json'}, body: JSON.stringify(payload) });
      showToast('Patient registered', 'Success', 'success');
      modalCard.remove();
      renderPatients();
    }catch(err){
      showToast(err.message || 'Unable to register', 'Error', 'danger');
    }
  });
}

// ===== Appointments =====
async function loadAppointments(){
  try{
    return await fetchJson(api.appointments);
  }catch(err){
    showToast('Unable to load appointments', 'Error', 'danger');
    return [];
  }
}

async function renderAppointments(){
  const app = qs('#app');
  app.innerHTML = '';
  const header = document.createElement('div');
  header.className = 'd-flex justify-content-between align-items-center mb-4';
  header.innerHTML = `
    <div>
      <h2 class="h4">Appointments</h2>
      <p class="muted mb-0">Upcoming appointments and schedule management</p>
    </div>
    <div>
      <button class="btn btn-outline-primary" id="refresh-appointments">Refresh</button>
    </div>
  `;
  app.appendChild(header);

  const listContainer = document.createElement('div');
  listContainer.id = 'appointments-list';
  app.appendChild(listContainer);

  const items = await loadAppointments();
  if (!items || items.length === 0) listContainer.innerHTML = '<div class="alert alert-info">No appointments scheduled</div>';
  else {
    const table = document.createElement('table');
    table.className = 'table table-hover';
    table.innerHTML = `<thead><tr><th>Patient</th><th>Date</th><th>Doctor</th><th>Status</th></thead>`;
    const tbody = document.createElement('tbody');
    items.forEach(a => {
      const tr = document.createElement('tr');
      tr.innerHTML = `<td>${escapeHtml(a.patientId)}</td><td>${new Date(a.appointmentDate).toLocaleString()}</td><td>${escapeHtml(a.doctorName)}</td><td>${escapeHtml(a.status)}</td>`;
      tbody.appendChild(tr);
    });
    table.appendChild(tbody);
    listContainer.appendChild(table);
  }

  qs('#refresh-appointments').addEventListener('click', () => renderAppointments());
}

// Schedule modal flow
function openScheduleModal(patientId){
  const modalEl = qs('#scheduleModal');
  const modal = new bootstrap.Modal(modalEl);
  qs('#sch-patientId').value = patientId;
  qs('#sch-date').value = '';
  qs('#sch-doctor').value = '';
  qs('#schedule-error').textContent = '';
  modal.show();
  qs('#schedule-form').onsubmit = async (ev) => {
    ev.preventDefault();
    const dto = {
      patientId: qs('#sch-patientId').value,
      appointmentDate: new Date(qs('#sch-date').value).toISOString(),
      doctorName: qs('#sch-doctor').value
    };
    try{
      await fetchJson(api.appointments, { method: 'POST', headers: {'Content-Type':'application/json'}, body: JSON.stringify(dto) });
      showToast('Appointment scheduled', 'Success', 'success');
      modal.hide();
      renderAppointments();
    }catch(err){
      qs('#schedule-error').textContent = err.message || 'Unable to schedule';
    }
  };
}

// Router
function navigate(route){
  if (route === 'appointments') renderAppointments();
  else renderPatients();
}

// Wire nav
document.addEventListener('DOMContentLoaded', () => {
  qsa('[data-route]').forEach(a => a.addEventListener('click', (e) => {
    e.preventDefault();
    const route = e.target.getAttribute('data-route');
    navigate(route);
  }));
  // start
  navigate('patients');
});

export {};
