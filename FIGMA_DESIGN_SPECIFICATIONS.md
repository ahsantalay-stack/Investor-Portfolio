# Portfolio Management System - Figma Design Specifications & User Flows

## 🎨 Design System Overview

### Color Palette (KSA-Themed)
```
Primary Colors:
- Saudi Green: #006C35 (Primary brand color)
- Gold Accent: #FFD700 (Secondary accent)
- White: #FFFFFF (Background)
- Off-White: #F8F9FA (Light background)

Text Colors:
- Dark Gray: #212529 (Primary text)
- Medium Gray: #6C757D (Secondary text)
- Light Gray: #ADB5BD (Disabled text)

Status Colors:
- Success: #28A745 (Approved, Paid)
- Warning: #FFC107 (Pending, Under Review)
- Danger: #DC3545 (Rejected, Failed)
- Info: #17A2B8 (Information)

Shariah Compliance:
- Halal Green: #2E8B57 (Shariah compliant indicator)
- Neutral: #6C757D (Non-specified)
```

### Typography
```
Primary Font: Inter (Modern, clean, supports Arabic)
- Headings: Inter Bold (24px, 20px, 18px, 16px)
- Body Text: Inter Regular (14px, 16px)
- Small Text: Inter Regular (12px, 10px)

Arabic Font: Cairo (Optimized for Arabic text)
- Arabic Headings: Cairo Bold
- Arabic Body: Cairo Regular

Font Weights:
- Light: 300
- Regular: 400
- Medium: 500
- Bold: 700
```

## 📱 Screen Designs & User Flows

### 1. **Investor Onboarding Flow**

#### 1.1 Landing Page
```
Components:
- Hero Section with Arabic/English toggle
- Value Proposition (Shariah Compliance, High Returns)
- Call-to-Action: "Start Investing" / "ابدأ الاستثمار"
- Trust Indicators (Licenses, Certifications)
- Language Toggle (EN/AR) in top-right

Layout:
- Header: Logo + Language Toggle + Login
- Hero: Background image of Saudi skyline
- Features: 3-column grid (Shariah, Returns, Security)
- Footer: Contact info, regulations
```

#### 1.2 Registration Form
```
Form Fields:
1. Personal Information
   - First Name (EN) + Last Name (EN)
   - First Name (AR) + Last Name (AR)
   - Email Address
   - Phone Number (+966 format)
   - Date of Birth (Islamic/Gregorian calendar)

2. Identity Verification
   - National ID Number (10-digit validation)
   - Nationality (Dropdown, default: Saudi Arabia)
   - City (Dropdown: Riyadh, Jeddah, Dammam, etc.)
   - Address (Text area)
   - Postal Code

3. Investment Profile
   - Risk Tolerance (Low/Medium/High slider)
   - Investment Goal (Dropdown)
   - Preferred Language (English/Arabic)

Validation:
- Real-time validation with error messages
- Saudi National ID format validation
- Strong password requirements
- Terms & Conditions checkbox (Shariah compliance)

Design:
- Progress indicator (Step 1 of 4)
- Clean, minimalist form design
- Green primary buttons
- Right-to-left support for Arabic fields
```

#### 1.3 KYC Document Upload
```
Document Types Required:
1. National ID (Front + Back)
2. Bank Statement (Last 3 months)
3. Proof of Income
4. Proof of Address

Upload Interface:
- Drag & drop zones for each document
- File type validation (PDF, JPG, PNG)
- File size limit indicators
- Preview thumbnails
- Progress bars for uploads
- Arabic document name support

Status Indicators:
- Not Uploaded (Gray)
- Uploading (Blue with progress)
- Uploaded (Green checkmark)
- Verification Pending (Orange)
- Verified (Green with badge)
- Rejected (Red with reason)

Design:
- Card-based layout for each document type
- Clear icons for document types
- Upload progress indicators
- Success/error messages
```

#### 1.4 Risk Assessment Questionnaire
```
Questions (5-7 questions):
1. Investment Experience Level
2. Financial Situation
3. Risk Tolerance Scenarios
4. Investment Timeline
5. Knowledge of Islamic Finance
6. Preferred Investment Types
7. Liquidity Requirements

Question Types:
- Multiple choice with visual indicators
- Slider inputs for percentage allocations
- Scenario-based questions with images
- Islamic finance knowledge assessment

Scoring:
- Automatic risk profile calculation
- Visual risk meter (Conservative to Aggressive)
- Recommended product categories
- Shariah compliance preferences

Design:
- One question per screen
- Progress indicator
- Visual scenarios with illustrations
- Clear navigation (Previous/Next)
- Risk level visualization
```

### 2. **Main Dashboard**

#### 2.1 Investor Dashboard
```
Key Metrics (Top Cards):
- Total Portfolio Value (SAR with trending chart)
- Total Profit Earned (with percentage growth)
- Active Investments count
- Next Expected Distribution

Main Sections:
1. Portfolio Overview
   - Pie chart of investment distribution
   - Performance chart (6 months)
   - Top performing investments

2. Recent Activity Feed
   - New investment confirmations
   - Profit distributions received
   - Document verification updates
   - System notifications

3. Quick Actions
   - Browse New Products
   - Make Investment
   - View Reports
   - Update Profile

4. Upcoming Events
   - Profit distribution dates
   - Investment maturity dates
   - KYC renewal reminders

Design:
- Clean, card-based layout
- Saudi Green accent colors
- Real-time data updates
- Responsive grid system
- Arabic number formatting for SAR
```

#### 2.2 Investment Products Marketplace
```
Filter Panel (Left Sidebar):
- Risk Level (Low/Medium/High)
- Return Frequency (Monthly/Quarterly/Yearly)
- Investment Amount Range (SAR slider)
- Expected Return Range (% slider)
- Category (Real Estate, Trade Finance, etc.)
- Shariah Compliance (Toggle - prominent)
- Availability Status

Product Cards:
- Product name (EN/AR)
- Expected return rate (prominent)
- Investment range (Min - Max SAR)
- Risk level indicator
- Shariah compliance badge
- Funding progress bar
- Time remaining indicator
- "Invest Now" button

Product Detail View:
- Hero image/video
- Detailed description (EN/AR)
- Investment details table
- Risk factors
- Shariah compliance certificate
- Historical performance
- Document downloads
- Investment calculator
- Similar products

Design:
- Search bar with filters
- Grid/List view toggle
- Sort options (Return, Risk, Funding %)
- Infinite scroll or pagination
- Shariah badge prominently displayed
```

### 3. **Investment Flow**

#### 3.1 Product Selection & Investment Amount
```
Investment Interface:
- Product summary card
- Investment amount input (SAR)
- Expected return calculation
- Investment timeline visualization
- Priority allocation explanation
- Terms and conditions

Validation:
- Minimum/maximum amount checks
- Available funding validation
- Investor eligibility checks
- Risk tolerance alignment

Calculation Display:
- Investment amount: X SAR
- Expected return: Y% annually
- Expected profit: Z SAR
- Distribution frequency: Monthly/Quarterly
- Maturity date: Date
- Priority rank estimate

Design:
- Large, clear number inputs
- Real-time calculations
- Progress indicators
- Investment summary card
- Prominent "Confirm Investment" button
```

#### 3.2 Investment Confirmation & Payment
```
Confirmation Screen:
- Investment summary
- Payment method selection
- Bank transfer details
- Terms acceptance
- Final confirmation

Payment Methods:
- Bank transfer (Primary)
- Digital wallet integration
- Direct debit authorization

Confirmation Flow:
1. Review investment details
2. Select payment method
3. Confirm terms and conditions
4. Submit investment request
5. Payment instructions
6. Confirmation receipt

Design:
- Clear step-by-step process
- Payment security indicators
- Digital receipt generation
- Email/SMS confirmation
```

### 4. **Portfolio Management**

#### 4.1 Investment Tracking
```
Investment List View:
- Investment cards with key metrics
- Status indicators (Active/Pending/Completed)
- Performance charts
- Profit distribution history
- Action buttons (View Details/Documents)

Investment Detail View:
- Investment overview
- Performance timeline
- Profit distribution schedule
- Transaction history
- Related documents
- Contact support

Performance Metrics:
- Current value
- Profit earned to date
- Return percentage
- Comparison to market
- Projected returns

Design:
- Tabular data with charts
- Interactive timeline
- Download options for reports
- Print-friendly layouts
```

#### 4.2 Profit Distribution Tracking
```
Distribution Overview:
- Upcoming distributions calendar
- Payment history table
- Cumulative profit charts
- Tax information summary

Distribution Details:
- Payment amount
- Payment date
- Payment method
- Tax deductions
- Reference numbers
- Receipt downloads

Calendar View:
- Monthly calendar with distribution dates
- Visual indicators for payment status
- Click-to-view distribution details
- Export to personal calendar

Design:
- Calendar integration
- Payment status indicators
- Download receipts functionality
- Tax summary cards
```

### 5. **Reports & Analytics**

#### 5.1 Comprehensive Reports
```
Report Types:
1. Investment Performance Report
2. Tax Report (with Zakat calculation)
3. Portfolio Distribution Report
4. Transaction History Report

Report Customization:
- Date range selection
- Investment filtering
- Language selection (EN/AR)
- Format options (PDF/Excel/CSV)

Zakat Calculation (Islamic Tax):
- Annual Zakat summary
- Zakat-eligible investments
- Calculation methodology
- Payment recommendations
- Religious guidance links

Design:
- Professional report layouts
- Charts and graphs
- Print-optimized formatting
- Digital signatures
- Watermarks for authenticity
```

#### 5.2 Analytics Dashboard
```
Analytics Views:
1. Portfolio Performance
   - ROI charts
   - Asset allocation
   - Risk distribution
   - Performance benchmarks

2. Investment Trends
   - Monthly investment patterns
   - Sector preferences
   - Risk appetite changes
   - Geographic distribution

3. Profit Analysis
   - Profit trends over time
   - Distribution frequency analysis
   - Tax efficiency metrics
   - Reinvestment opportunities

Interactive Elements:
- Date range pickers
- Chart type toggles
- Export functionality
- Drill-down capabilities

Design:
- Modern chart library integration
- Interactive data visualization
- Export to various formats
- Mobile-responsive charts
```

### 6. **Admin Dashboard**

#### 6.1 KYC Management
```
KYC Queue Interface:
- Pending verifications list
- Document viewer
- Verification tools
- Batch processing options
- Status update controls

Document Review:
- Side-by-side document comparison
- Zoom and annotation tools
- Verification checklists
- Rejection reason templates
- Communication tools

Workflow Management:
- Assignment to reviewers
- Priority queue management
- SLA tracking
- Performance metrics
- Audit trails

Design:
- Efficient workflow interface
- Quick action buttons
- Batch operation capabilities
- Search and filter options
```

#### 6.2 Investment Management
```
Investment Overview:
- All investments dashboard
- Priority allocation management
- Product performance monitoring
- Risk management tools

Priority Allocation Interface:
- Investment queue visualization
- Allocation rules configuration
- Manual override capabilities
- Fairness monitoring
- Performance impact analysis

Design:
- Data-rich interfaces
- Real-time updates
- Workflow optimization
- Compliance monitoring
```

## 🔄 User Flow Diagrams

### Primary User Flows

1. **Investor Onboarding Flow**
   ```
   Landing Page → Registration → Email Verification → 
   KYC Documents → Risk Assessment → Dashboard Access
   ```

2. **Investment Flow**
   ```
   Dashboard → Browse Products → Filter/Search → 
   Product Details → Investment Amount → Confirmation → 
   Payment → Investment Tracking
   ```

3. **KYC Verification Flow**
   ```
   Document Upload → Verification Queue → 
   Review Process → Approval/Rejection → 
   Notification → Status Update
   ```

4. **Profit Distribution Flow**
   ```
   Profit Calculation → Distribution Processing → 
   Payment Generation → Investor Notification → 
   Receipt Generation → Reporting
   ```

## 📐 Component Library

### Reusable Components

1. **Investment Card**
   - Product image
   - Return rate badge
   - Shariah compliance indicator
   - Investment details
   - Action buttons

2. **KYC Status Indicator**
   - Status badge
   - Progress indicator
   - Action required alerts
   - Document thumbnails

3. **Payment Method Selector**
   - Bank transfer option
   - Digital wallet integration
   - Security indicators
   - Payment instructions

4. **Language Toggle**
   - EN/AR switcher
   - RTL layout changes
   - Font switching
   - Cultural adaptations

## 🌍 Localization Design Guidelines

### Arabic (RTL) Layout Considerations

1. **Text Direction**
   - Right-to-left reading flow
   - Icon positioning adjustments
   - Navigation menu alignment
   - Form label positioning

2. **Cultural Adaptations**
   - Conservative imagery
   - Islamic calendar integration
   - Prayer time considerations
   - Cultural color preferences

3. **Typography**
   - Arabic font optimization
   - Line height adjustments
   - Character spacing
   - Mixed language handling

## 📱 Responsive Design Breakpoints

```
Mobile: 320px - 767px
Tablet: 768px - 1023px
Desktop: 1024px - 1439px
Large Desktop: 1440px+

Key Adaptations:
- Mobile-first approach
- Touch-friendly interfaces
- Simplified navigation
- Optimized forms
- Readable typography
```

## 🎯 Accessibility Guidelines

1. **WCAG 2.1 AA Compliance**
   - Color contrast ratios
   - Keyboard navigation
   - Screen reader compatibility
   - Alternative text for images

2. **Arabic Accessibility**
   - Right-to-left screen reader support
   - Arabic voice commands
   - Cultural context awareness
   - Religious considerations

## 🔍 Usability Testing Plans

### Test Scenarios

1. **New Investor Onboarding**
   - Account creation flow
   - KYC document submission
   - First investment process

2. **Portfolio Management**
   - Investment tracking
   - Performance monitoring
   - Report generation

3. **Cross-cultural Testing**
   - Arabic language flow
   - Cultural appropriateness
   - Religious compliance

This comprehensive design specification provides the foundation for creating a modern, accessible, and culturally appropriate Portfolio Management system specifically tailored for the Saudi Arabian market.