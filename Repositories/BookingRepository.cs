using ccse_cw1.Models;
using ccse_cw1.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace ccse_cw1.Repositories
{
    public class BookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Hotel> GetHotels()
        {
            var hotels = _context.Hotels.ToList();

            return hotels;
        }

        public List<Tour> GetTours()
        { 
            var tours = _context.Tours.ToList();
            
            return tours;
        }

        public async Task<Booking> GetBookingAsync(int bookingId)
        {
            var booking = await _context.Booking.FirstOrDefaultAsync(b => b.BookingID == bookingId);

            return booking;
        }
        public int GetBookedRoomCount(int hotelId, string roomType, DateTime checkIn, DateTime checkOut)
        {
            
            return _context.Booking
                .Count(b => b.HotelID == hotelId &&
                            b.RoomType == roomType &&
                            !(b.CheckOut <= checkIn || b.CheckIn >= checkOut));
        }

        public async Task<Booking> CreateBookingAsync(string userid, DateTime checkin, DateTime checkout, DateTime? tourstart, int hotelid = 0, string roomtype = "", int tourid = 0, int roomnumber = 0)
        {
            var cost = 0;
            var discount = 0;

            var tour = _context.Tours.FirstOrDefault(h => h.TourID == tourid);
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelID == hotelid);

            if (hotel != null)
            {
                if (roomtype == "single")
                {
                    discount = 10;
                    cost += hotel.SinglePrice;
                    roomnumber = +1;
                }
                else if (roomtype == "double")
                {
                    discount = 20;
                    cost += hotel.DoublePrice;
                    roomnumber += 1;
                }
                else if (roomtype == "family")
                {
                    discount = 40;
                    cost += hotel.FamilyPrice;
                    roomnumber += 1;
                }
            }


            if (hotelid != 0 & tourid != 0)
            {
                if (tour != null)
                {
                    cost += tour.TourPrice;

                    cost -= cost * (discount / 100);

                    var booking = new Booking
                    {
                        HotelID = hotelid,
                        UserID = userid,
                        TourStart = (DateTime)tourstart,
                        CheckOut = checkout,
                        RoomType = roomtype,
                        RoomNumber = roomnumber,
                        CreatedAt = DateTime.Now,
                        TourID = tourid,
                        Discount = discount,
                        Cost = cost,
                    };

                    await _context.Booking.AddAsync(booking);
                    await _context.SaveChangesAsync();

                    return booking;
                }
            }
            else if (hotelid != 0)
            {
                var booking = new Booking
                {
                    HotelID = hotelid,
                    UserID = userid,
                    CheckIn = checkin,
                    CheckOut = checkout,
                    RoomType = roomtype,
                    RoomNumber = roomnumber,
                    CreatedAt = DateTime.Now,
                    TourID = tourid,
                    Discount = 0,
                    Cost = cost,
                };

                await _context.Booking.AddAsync(booking);
                await _context.SaveChangesAsync();

                return booking;
            }
            else if (tourid != 0)
            {
                cost += tour.TourPrice;

                var booking = new Booking
                {
                    HotelID = hotelid,
                    UserID = userid,
                    CheckIn = checkin,
                    CheckOut = checkin.AddDays(tour.Duration),
                    CreatedAt = DateTime.Now,
                    TourID = tourid,
                    Discount = 0,
                    Cost = cost,
                };

                await _context.Booking.AddAsync(booking);
                await _context.SaveChangesAsync();

                return booking;
            }
            

            return new Booking
            {
                CreatedAt = DateTime.MinValue
            };
        }

        internal Task<Booking> CreateBookingAsync(string id, DateTime checkIn, DateTime checkOut, DateTime? tourStart, int tourid, string roomType)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            try
            {
                var bookingToDelete = await _context.Booking.FindAsync(bookingId);

                if (bookingToDelete != null)
                {
                    _context.Booking.Remove(bookingToDelete);
                    await _context.SaveChangesAsync();
                    return true; // Booking successfully deleted
                }

                return false; // Booking not found
            }
            catch (Exception)
            {
                // Handle exceptions if necessary
                return false; // Deletion failed
            }
        }

    }
}
