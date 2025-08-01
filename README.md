# Portfolio Management System API

A comprehensive **Portfolio Management System** built for **Loan Origination and Management** with **Investor KYC**, **Investment Products Marketplace**, **Priority Allocation System**, and **Profit Distribution Tracking**. Specifically designed for the **Kingdom of Saudi Arabia (KSA)** market with **Shariah compliance** and **Arabic language support**.

## 🇸🇦 KSA Market Features

- **Shariah Compliance**: All investment products are marked and filtered for Islamic compliance
- **Arabic Language Support**: Dual language support (English/Arabic) with RTL text support
- **SAR Currency**: All financial calculations in Saudi Riyals
- **Local Regulations**: KYC requirements tailored for Saudi market (National ID, local banking)
- **Zakat Calculation**: Automatic Islamic tax calculation for investors
- **Regional Analytics**: Investment distribution across KSA regions

## 🚀 Key Features

### 1. **Investor Management & KYC**
- Complete investor onboarding with KYC verification
- Document upload and verification (National ID, Bank statements, etc.)
- Risk assessment and investor profiling
- Arabic name support and dual language interface

### 2. **Investment Products Marketplace**
- **Shariah-compliant** financing products
- Advanced filtering by risk level, return terms, and investment amounts
- **Priority allocation system** (lowest return investors get priority)
- Comprehensive product documentation and certificates

### 3. **Priority Allocation System**
- Intelligent investment allocation based on **lowest expected return**
- Investors with lower return expectations get priority in funding
- Automatic priority ranking and queue management
- Multi-product investment support

### 4. **Portfolio Management**
- Real-time portfolio tracking and performance analytics
- Investment distribution visualization
- Monthly, quarterly, and yearly return calculations
- Comprehensive transaction history

### 5. **Profit Distribution & Tracking**
- Automated profit calculation and distribution
- Flexible return terms (Monthly, Quarterly, Yearly)
- Company and investor profit sharing
- Payment processing and tracking

### 6. **Reports & Analytics**
- Comprehensive investor reports with **tax information**
- **Zakat calculation** (Islamic obligation)
- Company-wide analytics and performance metrics
- Export functionality (PDF, Excel, CSV)
- Real-time dashboard analytics

## 🏗️ Architecture

The system follows **Clean Architecture** principles with clear separation of concerns:

```
├── src/
│   ├── PortfolioManagement.API/          # Web API Controllers & Configuration
│   ├── PortfolioManagement.Application/  # Business Logic & Services
│   ├── PortfolioManagement.Core/         # Domain Entities & Interfaces
│   └── PortfolioManagement.Infrastructure/ # Data Access & External Services
```

### Technology Stack

- **.NET 8.0** - Modern, high-performance web API framework
- **Entity Framework Core** - ORM for data access
- **SQL Server** - Primary database
- **AutoMapper** - Object-to-object mapping
- **FluentValidation** - Input validation
- **Swagger/OpenAPI** - API documentation
- **JWT Authentication** - Security (planned)

## 📊 API Endpoints

### **Investors Management**
- `POST /api/investors` - Create new investor
- `GET /api/investors/{id}` - Get investor details
- `PUT /api/investors/{id}` - Update investor profile
- `GET /api/investors/by-email/{email}` - Find by email
- `GET /api/investors/by-national-id/{nationalId}` - Find by National ID (KSA)

### **KYC Operations**
- `POST /api/investors/{id}/kyc/documents` - Upload KYC documents
- `GET /api/investors/{id}/kyc/status` - Get KYC verification status
- `POST /api/investors/{id}/kyc/approve` - Approve KYC (Admin)
- `POST /api/investors/{id}/kyc/reject` - Reject KYC (Admin)

### **Investment Products**
- `GET /api/financingproducts` - Get products with advanced filtering
- `GET /api/financingproducts/sharia-compliant` - Get Shariah-compliant products
- `GET /api/financingproducts/recommended/{investorId}` - Personalized recommendations
- `GET /api/financingproducts/{id}/availability` - Check investment availability

### **Investments & Portfolio**
- `POST /api/investments/investors/{investorId}` - Create new investment
- `GET /api/investments/investors/{investorId}` - Get investor's investments
- `GET /api/investments/investors/{investorId}/portfolio` - Portfolio performance
- `GET /api/investments/investors/{investorId}/dashboard` - Dashboard data

### **Priority Allocation**
- `POST /api/investments/products/{productId}/allocate` - Process allocation
- `GET /api/investments/products/{productId}/priority-queue` - View priority queue
- `POST /api/investments/products/{productId}/recalculate-priorities` - Recalculate ranks

### **Profit Distribution**
- `GET /api/profitdistributions/investors/{investorId}` - Get profit distributions
- `POST /api/profitdistributions/process-payment` - Process profit payment
- `POST /api/profitdistributions/generate/monthly` - Generate monthly distributions
- `GET /api/profitdistributions/company-summary` - Company profit summary

### **Reports & Analytics**
- `GET /api/reports/investors/{investorId}` - Comprehensive investor report
- `GET /api/reports/tax/investors/{investorId}/{year}` - Tax report with Zakat
- `GET /api/reports/company/analytics` - Company-wide analytics
- `GET /api/reports/regional-distribution` - KSA regional distribution
- `POST /api/reports/export` - Export reports (PDF/Excel/CSV)

## 🔧 Getting Started

### Prerequisites
- **.NET 8.0 SDK**
- **SQL Server** (LocalDB for development)
- **Visual Studio 2022** or **VS Code**

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd portfolio-management
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Update connection string** (when database is configured)
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PortfolioManagementDb;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

4. **Run the application**
   ```bash
   cd src/PortfolioManagement.API
   dotnet run
   ```

5. **Access Swagger Documentation**
   - Navigate to `https://localhost:5001` (or the displayed port)
   - Interactive API documentation will be available

## 📈 Business Logic

### Priority Allocation System

The system implements a **unique priority allocation mechanism**:

1. **Investors specify expected return rate** when creating investments
2. **System ranks investors by lowest expected return** (ascending order)
3. **Priority allocation** ensures investors with lower return expectations get funded first
4. **Automatic rebalancing** when new investments are made or products are updated

### Profit Distribution

- **Flexible Terms**: Monthly, Quarterly, or Yearly distributions
- **Profit Sharing**: Configurable split between company and investors
- **Automated Calculation**: System calculates profits based on investment performance
- **Payment Tracking**: Complete audit trail of all profit payments

### Shariah Compliance

- **Product Certification**: All products marked with Shariah compliance status
- **Filtering**: Investors can filter for Islamic-compliant investments only
- **Documentation**: Shariah certificates and compliance documents attached to products
- **Reporting**: Separate reporting for Islamic investments

## 🌍 Localization (KSA Specific)

### Arabic Language Support
- **Dual Language Fields**: English and Arabic names for investors and products
- **RTL Support**: Right-to-left text rendering for Arabic content
- **Date Formatting**: Hijri calendar support (planned)
- **Cultural Considerations**: Conservative, professional design approach

### Saudi Market Features
- **National ID Validation**: Saudi National ID format validation
- **Regional Analytics**: Investment distribution across Saudi regions
- **Local Banking**: IBAN and local bank account support
- **Currency**: All amounts in Saudi Riyals (SAR)

## 📊 Sample API Responses

### Investor Dashboard Data
```json
{
  "totalInvestedAmount": 150000.00,
  "totalProfitEarned": 12500.00,
  "activeInvestments": 3,
  "overallReturnRate": 8.33,
  "recentInvestments": [...],
  "upcomingDistributions": [...],
  "portfolioDistribution": {
    "realEstate": 60000.00,
    "tradeFinance": 90000.00
  }
}
```

### Investment Product with Shariah Compliance
```json
{
  "id": "guid",
  "name": "Real Estate Development Fund",
  "nameAr": "صندوق تطوير العقارات",
  "expectedReturnRate": 12.5,
  "minInvestmentAmount": 50000.00,
  "isShariaCompliant": true,
  "shariaComplianceCertificate": "certificate-url",
  "availableAmount": 2500000.00,
  "riskLevel": "Medium",
  "returnFrequency": "Quarterly"
}
```

## 🔐 Security Features

- **JWT Authentication** (planned implementation)
- **Role-based Authorization** (Investor, Admin, Compliance Officer)
- **Data Encryption** for sensitive information
- **Audit Logging** for all financial transactions
- **Rate Limiting** and **CORS** configuration
- **Input Validation** with FluentValidation

## 📋 Planned Enhancements

- **Database Implementation** with Entity Framework migrations
- **Service Layer Implementation** with business logic
- **Authentication & Authorization** with JWT
- **File Storage** for document uploads (Azure Blob Storage)
- **Payment Gateway Integration** for Saudi banks
- **Email Notifications** for profit distributions
- **Mobile App Support** with dedicated endpoints
- **Real-time Updates** with SignalR

## 🎨 Frontend Integration

The API is designed to work with the provided frontend:
- **Base URL**: `https://preview-minimalist-newsletter-form-kzmqurflb6z6j6kgtruc.vusercontent.net`
- **CORS Enabled** for cross-origin requests
- **RESTful Design** for easy frontend integration
- **Consistent Response Format** across all endpoints

## 📞 Support

For technical support or business inquiries:
- **Email**: support@portfoliomanagement.sa
- **Documentation**: Available at `/swagger` endpoint
- **Health Check**: Available at `/health` endpoint

---

**Built for the KSA market with Shariah compliance and Arabic language support** 🇸🇦