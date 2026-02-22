export interface Patient {
  id: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  email: string;
  phoneNumber: string;
  isActive: boolean;
}

export enum AppointmentStatus {
  Scheduled,
  CheckedIn,
  Completed,
  Cancelled,
  NoShow
}

export interface Appointment {
  id: string;
  patientId: string;
  appointmentDate: string;
  doctorName: string;
  status: AppointmentStatus;
}
