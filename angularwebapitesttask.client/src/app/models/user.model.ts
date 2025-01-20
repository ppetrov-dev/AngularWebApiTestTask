export interface CreateUser {
  login: string;
  password: string;
  provinceId: number;
}

export interface CreateUserResult {
  login: string;
  error: string;
}

export interface UserResult {
  id: number;
  login: string;
  provinceName: string;
  countryName: string;
}
